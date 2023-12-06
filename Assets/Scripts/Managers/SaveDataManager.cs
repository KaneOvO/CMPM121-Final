using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Unity.VisualScripting;

public class LandAreaSaver : MonoBehaviour
{
    public LandArea landArea;

    private float timer = 0f;
    private float interval = 10f;
    public void SaveLandArea(string filePath)
    {
        landArea = PlantManager.landArea;
        if (landArea == null)
        {
            Debug.LogError("LandArea is null!");
            return;
        }

        SerializableLandCellArray serializableArray = new SerializableLandCellArray
        {
            cells = new SerializableLandCell[landArea.landCells.Length]
        };

        for (int i = 0; i < landArea.landCells.Length; i++)
        {
            serializableArray.cells[i] = ConvertToSerializable(landArea.landCells[i]);
        }
        serializableArray.currentTurn = GameManager.Instance.currentTurn;
        serializableArray.numOfCarrot = PlantManager.Instance.numOfCarrot;
        serializableArray.numOfCabbage = PlantManager.Instance.numOfCabbage;
        serializableArray.numOfOnion = PlantManager.Instance.numOfOnion;

        string json = JsonUtility.ToJson(serializableArray);
        File.WriteAllText(filePath, json);
        Debug.Log("LandArea saved to " + filePath);
    }


    public LandArea LoadLandArea(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new LandArea();
        }

        string json = File.ReadAllText(filePath);
        SerializableLandCellArray serializableArray = JsonUtility.FromJson<SerializableLandCellArray>(json);

        LandArea landArea = new LandArea();
        int lands = GlobalValue.LAND_NUM;
        byte[] landBuffer = new byte[lands * LandCell.NumBytes];
        for (int i = 0; i < lands; i++)
        {
            landArea.addCell(new LandCell(landBuffer, i * LandCell.NumBytes)
            {
                isPanted = serializableArray.cells[i].isPanted,
                landPlantedType = serializableArray.cells[i].landPlantedType,
                currentStage = serializableArray.cells[i].currentStage,
                water = serializableArray.cells[i].water,
            });
        }

        GameManager.Instance.currentTurn = serializableArray.currentTurn;
        PlantManager.Instance.numOfCarrot = serializableArray.numOfCarrot;
        PlantManager.Instance.numOfCabbage = serializableArray.numOfCabbage;
        PlantManager.Instance.numOfOnion = serializableArray.numOfOnion;

        return landArea;
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
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            SavedataAuto();
            timer = 0f;
        }
    }

    public void SavedataAuto()
    {
        Debug.Log("Auto Save");
        SaveLandArea(Application.persistentDataPath + "/landAreaSaveAuto.json");
    }

    public void Savedata1()
    {
        SaveLandArea(Application.persistentDataPath + "/landAreaSave1.json");
    }

    public void Savedata2()
    {
        SaveLandArea(Application.persistentDataPath + "/landAreaSave2.json");
    }

    public void LoaddataAuto()
    {
        Debug.Log("Auto Load");
        PlantManager.landArea = LoadLandArea(Application.persistentDataPath + "/landAreaSaveAuto.json");
    }

    public void Loaddata1()
    {
        PlantManager.landArea = LoadLandArea(Application.persistentDataPath + "/landAreaSave1.json");
    }

    public void Loaddata2()
    {
        PlantManager.landArea = LoadLandArea(Application.persistentDataPath + "/landAreaSave2.json");
    }

    

}