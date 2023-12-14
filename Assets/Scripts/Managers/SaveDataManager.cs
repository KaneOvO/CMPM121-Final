using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Unity.VisualScripting;

public class LandAreaSaver : MonoBehaviour
{
    public LandArea landArea;

    public void SaveUndo(string filePath, Stack<Savedata> stack)
    {
        landArea = PlantManager.landArea;
        if (landArea == null)
        {
            Debug.LogError("LandArea is null!");
            return;
        }

        // Get undo file path
        string undo_filePath = Application.persistentDataPath + filePath;

        // if undo file does not exist, then save data
        if (!File.Exists(undo_filePath))
        {
            SerializeStackToJSON(stack, Application.persistentDataPath + filePath);
            Debug.Log("Saved Undo");
            return;
        }

        // Read JSON from undo file
        string un_json = File.ReadAllText(undo_filePath);

        // Deserialize JSON to SerializableDataWrapper object
        SerializableDataWrapper un_wrapper = JsonUtility.FromJson<SerializableDataWrapper>(un_json);

        // if deserialization failed or no data present, then save data
        if (un_wrapper == null || un_wrapper.data == null || un_wrapper.data.Count == 0)
        {
            SerializeStackToJSON(stack, Application.persistentDataPath + filePath);
            Debug.Log("Saved Undo");
            return;
        }

        // Convert the last peek of undo stack to Savedata object
        Savedata undoPeek = ConvertToSavedata(un_wrapper.data[un_wrapper.data.Count - 1]);

        // Compare the last peek of undo stack with the current game state
        bool isSame = IsGameStateSame(undoPeek, new Savedata(landArea, GameManager.Instance.currentTurn, PlantManager.Instance.numOfCarrot, PlantManager.Instance.numOfCabbage, PlantManager.Instance.numOfOnion));
        if (!isSame)
        {
            SerializeStackToJSON(stack, Application.persistentDataPath + filePath);
            Debug.Log("Saved Undo");
        }

    }

    public void SaveRedo(string filePath, Stack<Savedata> stack)
    {
        landArea = PlantManager.landArea;
        if (landArea == null)
        {
            Debug.LogError("LandArea is null!");
            return;
        }

        SerializeStackToJSON(stack, Application.persistentDataPath + filePath);
        Debug.Log("Saved Redo");

    }


    public LandArea LoadLandArea(string filePath, Stack<Savedata> undostack, Stack<Savedata> redostack)
    {
        string undo_filePath = Application.persistentDataPath + filePath + "_undo.json";
        if (!File.Exists(undo_filePath))
        {
            return new LandArea();
        }
        string un_json = File.ReadAllText(undo_filePath);
        SerializableDataWrapper un_wrapper = JsonUtility.FromJson<SerializableDataWrapper>(un_json);
        if (un_wrapper == null || un_wrapper.data == null)
        {
            return new LandArea(); // Or handle the error as appropriate
        }
        undostack.Clear();
        foreach (var serializableArray in un_wrapper.data)
        {
            Savedata savedata = ConvertToSavedata(serializableArray);
            undostack.Push(savedata);
        }
        GameManager.Instance.carrotNeeded = un_wrapper.carrotNeeded;
        GameManager.Instance.cabbageNeeded = un_wrapper.cabbageNeeded;
        GameManager.Instance.onionNeeded = un_wrapper.onionNeeded;
        GameManager.Instance.maxTurns = un_wrapper.maxTurns;
        
        FindObjectOfType<SetLanguage>().updateLanguage(un_wrapper.language);


        string redo_filePath = Application.persistentDataPath + filePath + "_redo.json";
        if (!File.Exists(redo_filePath))
        {
            return new LandArea();
        }
        string re_json = File.ReadAllText(redo_filePath);
        SerializableDataWrapper re_wrapper = JsonUtility.FromJson<SerializableDataWrapper>(re_json);
        if (re_wrapper == null || re_wrapper.data == null)
        {
            return new LandArea(); // Or handle the error as appropriate
        }
        redostack.Clear();
        foreach (var serializableArray in re_wrapper.data)
        {
            Savedata savedata = ConvertToSavedata(serializableArray);
            redostack.Push(savedata);
        }

        Savedata latest = undostack.Pop();
        LandArea landArea = latest.landArea;
        GameManager.Instance.currentTurn = latest.currentTurn;
        PlantManager.Instance.numOfCarrot = latest.numberOfCarrot;
        PlantManager.Instance.numOfCabbage = latest.numberOfCabbage;
        PlantManager.Instance.numOfOnion = latest.numberOfOnion;
        return landArea;
    }

    private Savedata ConvertToSavedata(SerializableLandCellArray serializableArray)
    {
        LandArea landArea = ConvertToLandArea(serializableArray.cells);
        return new Savedata(landArea, serializableArray.currentTurn,
                            serializableArray.numOfCarrot, serializableArray.numOfCabbage,
                            serializableArray.numOfOnion);
    }

    private LandArea ConvertToLandArea(SerializableLandCell[] serializableCells)
    {
        LandArea landArea = new LandArea();
        byte[] buffer = new byte[GlobalValue.LAND_NUM * LandCell.NumBytes];
        for (int i = 0; i < serializableCells.Length; i++)
        {
            var serializableCell = serializableCells[i];

            landArea.addCell(new LandCell(buffer, i * LandCell.NumBytes)
            {
                isPanted = serializableCell.isPanted,
                landPlantedType = serializableCell.landPlantedType,
                currentStage = serializableCell.currentStage,
                water = serializableCell.water
            });

        }
        return landArea;
    }


    public void SerializeStackToJSON(Stack<Savedata> stack, string filePath)
    {
        int count = 0;
        if (stack == null || stack.Count == 0)
        {
            File.WriteAllText(filePath, "{}");
            return;
        }

        List<SerializableLandCellArray> serializedDataList = new List<SerializableLandCellArray>();
        foreach (Savedata savedata in stack)
        {
            count += 1;
            if (savedata == null || savedata.landArea == null || savedata.landArea.landCells == null)
            {
                continue; // Skip this item
            }

            SerializableLandCellArray serializableArray = new SerializableLandCellArray
            {
                cells = new SerializableLandCell[savedata.landArea.landCells.Length],
                currentTurn = savedata.currentTurn,
                numOfCarrot = savedata.numberOfCarrot,
                numOfCabbage = savedata.numberOfCabbage,
                numOfOnion = savedata.numberOfOnion
            };

            for (int i = 0; i < savedata.landArea.landCells.Length; i++)
            {
                if (savedata.landArea.landCells[i] == null)
                {
                    Debug.Log("Found a null LandCell in the stack. Skipping this cell.");
                    continue; // Skip this cell
                }

                serializableArray.cells[i] = ConvertToSerializable(savedata.landArea.landCells[i]);
            }

            serializedDataList.Add(serializableArray);
        }
        serializedDataList.Reverse();
        SerializableDataWrapper wrapper = new SerializableDataWrapper
        {
            carrotNeeded = GameManager.Instance.carrotNeeded,
            cabbageNeeded = GameManager.Instance.cabbageNeeded,
            onionNeeded = GameManager.Instance.onionNeeded,
            maxTurns = GameManager.Instance.maxTurns,
            language = FindObjectOfType<SetLanguage>().currentLanguage,
            data = serializedDataList
        };
        string jsonArray = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(filePath, jsonArray);
    }

    private SerializableLandCell ConvertToSerializable(LandCell landCell)
    {
        return new SerializableLandCell
        {
            isPanted = landCell.isPanted,
            landPlantedType = landCell.landPlantedType,
            currentStage = landCell.currentStage,
            water = landCell.water
        };
    }

    private void OnApplicationQuit()
    {
        SavedataAuto();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SavedataAuto();
        }
    }

    public void SavedataAuto()
    {
        if (GameManager.Instance.currentTurn <= GameManager.Instance.maxTurns)
        {
            GameManager.Instance.SaveCureentSituations();
            SaveUndo("/landAreaSaveAuto_undo.json", GameManager.undoStack);
            SaveRedo("/landAreaSaveAuto_redo.json", GameManager.redoStack);
        }

    }

    public void Savedata1()
    {
        if (GameManager.Instance.currentTurn <= GameManager.Instance.maxTurns)
        {
            GameManager.Instance.SaveCureentSituations();
            SaveUndo("/landAreaSave1_undo.json", GameManager.undoStack);
            SaveRedo("/landAreaSave1_redo.json", GameManager.redoStack);
        }
    }

    public void Savedata2()
    {
        if (GameManager.Instance.currentTurn <= GameManager.Instance.maxTurns)
        {
            GameManager.Instance.SaveCureentSituations();
            SaveUndo("/landAreaSave2_undo.json", GameManager.undoStack);
            SaveRedo("/landAreaSave2_redo.json", GameManager.redoStack);
        }
    }

    public void LoaddataAuto()
    {
        Debug.Log("Auto Load");
        PlantManager.landArea = LoadLandArea("/landAreaSaveAuto", GameManager.undoStack, GameManager.redoStack);

    }

    public void Loaddata1()
    {
        PlantManager.landArea = LoadLandArea("/landAreaSave1", GameManager.undoStack, GameManager.redoStack);
    }

    public void Loaddata2()
    {
        PlantManager.landArea = LoadLandArea("/landAreaSave2", GameManager.undoStack, GameManager.redoStack);
    }

    private bool IsGameStateSame(Savedata savedata1, Savedata savedata2)
    {
        if (savedata1.currentTurn != savedata2.currentTurn) return false;
        if (savedata1.numberOfCarrot != savedata2.numberOfCarrot) return false;
        if (savedata1.numberOfCabbage != savedata2.numberOfCabbage) return false;
        if (savedata1.numberOfOnion != savedata2.numberOfOnion) return false;
        if (!savedata1.landArea.Equals(savedata2.landArea)) return false;

        return true;
    }

    [System.Serializable]
    private class Serialization<T>
    {
        public T[] items;
        public Serialization(T[] items) { this.items = items; }
    }

}
