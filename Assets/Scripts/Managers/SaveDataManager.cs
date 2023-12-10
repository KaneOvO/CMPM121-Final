using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Unity.VisualScripting;

public class LandAreaSaver : MonoBehaviour
{
    public LandArea landArea;

    // private float timer = 0f;
    // private float interval = 10f;
    
    public void SaveLandArea(string filePath, Stack<Savedata> stack)
    {
        landArea = PlantManager.landArea;
        if (landArea == null)
        {
            Debug.LogError("LandArea is null!");
            return;
        }
        SerializeStackToJSON(stack, Application.persistentDataPath+filePath);
    }


    public LandArea LoadLandArea(string filePath, Stack<Savedata> undostack, Stack<Savedata> redostack)
    {
        string undo_filePath=Application.persistentDataPath+filePath+"_undo.json";
        if (!File.Exists(undo_filePath))
        {
            return new LandArea();
        }
        string un_json = File.ReadAllText(undo_filePath);
        SerializableDataWrapper un_wrapper = JsonUtility.FromJson<SerializableDataWrapper>(un_json);
        if (un_wrapper == null || un_wrapper.data == null)
        {
            Debug.LogError("Failed to deserialize JSON or no data present.");
            return new LandArea(); // Or handle the error as appropriate
        }
        undostack.Clear();
        foreach (var serializableArray in un_wrapper.data){
            Savedata savedata = ConvertToSavedata(serializableArray);
            undostack.Push(savedata);
        }


        string redo_filePath=Application.persistentDataPath+filePath+"_redo.json";
        if (!File.Exists(redo_filePath))
        {
            return new LandArea();
        }
        string re_json = File.ReadAllText(redo_filePath);
        SerializableDataWrapper re_wrapper = JsonUtility.FromJson<SerializableDataWrapper>(re_json);
        if (re_wrapper == null || re_wrapper.data == null)
        {
            Debug.LogError("Failed to deserialize JSON or no data present.");
            return new LandArea(); // Or handle the error as appropriate
        }
        redostack.Clear();
        foreach (var serializableArray in re_wrapper.data){
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
        int count=0;
        if (stack == null || stack.Count == 0)
        {
            //Debug.Log("The stack is empty. No data to serialize.");
            return;
        }
        
        List<SerializableLandCellArray> serializedDataList = new List<SerializableLandCellArray>();
        foreach (Savedata savedata in stack)
        {
            count+=1;
            if (savedata == null || savedata.landArea == null || savedata.landArea.landCells == null)
            {
                //Debug.Log("Found null or invalid data in the stack. Skipping an item.");
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

    private void Update()
    {

    }

    private void OnApplicationQuit()
    {
        SavedataAuto();
    }

    public void SavedataAuto()
    {
        GameManager.Instance.SaveCureentSituations();
        SaveLandArea("/landAreaSaveAuto_undo.json", GameManager.undoStack);
        SaveLandArea("/landAreaSaveAuto_redo.json", GameManager.redoStack);
    }

    public void Savedata1()
    {
        GameManager.Instance.SaveCureentSituations();
        SaveLandArea("/landAreaSave1_undo.json", GameManager.undoStack);
        SaveLandArea("/landAreaSave1_redo.json", GameManager.redoStack);
    }

    public void Savedata2()
    { 
        GameManager.Instance.SaveCureentSituations();
        SaveLandArea("/landAreaSave2_undo.json", GameManager.undoStack);
        SaveLandArea("/landAreaSave2_redo.json", GameManager.redoStack);
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

    [System.Serializable]
    private class Serialization<T>
    {
        public T[] items;
        public Serialization(T[] items) { this.items = items; }
    }

}
