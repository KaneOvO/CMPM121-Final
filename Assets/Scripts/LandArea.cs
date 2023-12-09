using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LandArea
{
    public LandCell[] landCells = new LandCell[GlobalValue.LAND_NUM];
    private int index = GlobalValue.FIRST_INDEX;

    public LandCell GetLandCell(int i)
    {
        LandCell landCell = landCells[i];
        if (landCell != null) return landCell;

        byte[] landBuffer = new byte[GlobalValue.LAND_NUM * LandCell.NumBytes];
        return new LandCell(landBuffer, i * LandCell.NumBytes)
        {
            isPanted = false,
            landPlantedType = PlantType.EMPTY,
            currentStage = GlobalValue.INITIAL_STAGE,
            water = RandomResources.GetRandom(),
        };
    }

    public LandArea(LandArea other)
    {
        this.landCells = new LandCell[GlobalValue.LAND_NUM];
        for (int i = 0; i < GlobalValue.LAND_NUM; i++)
        {
            if (other.landCells[i] != null)
            {
                this.landCells[i] = new LandCell(other.landCells[i]);
            }
        }
        this.index = other.index;
    }

    public LandArea()
    {

    }

    public void addCell(LandCell landCell)
    {
        landCells[index] = landCell;
        if (index < GlobalValue.LAND_NUM)
        {
            index++;
        }
    }

    public void removeCell()
    {
        byte[] landBuffer = new byte[GlobalValue.LAND_NUM * LandCell.NumBytes];
        landCells[index] = new LandCell(landBuffer, index * LandCell.NumBytes)
        {
            isPanted = false,
            landPlantedType = PlantType.EMPTY,
            currentStage = GlobalValue.INITIAL_STAGE,
            water = RandomResources.GetRandom(),
        };
        if (index != GlobalValue.FIRST_INDEX)
        {
            index--;
        }
    }

    public void removeCell(int i)
    {
        byte[] landBuffer = new byte[GlobalValue.LAND_NUM * LandCell.NumBytes];
        landCells[i] = new LandCell(landBuffer, i * LandCell.NumBytes)
        {
            isPanted = false,
            landPlantedType = PlantType.EMPTY,
            currentStage = GlobalValue.INITIAL_STAGE,
            water = RandomResources.GetRandom(),
        };
    }

}

[System.Serializable]
public class SerializableLandCell
{
    public bool isPanted;
    public PlantType landPlantedType;
    public int currentStage;
    public float water;
}

[System.Serializable]
public class SerializableLandCellArray
{
    public SerializableLandCell[] cells;
    public int currentTurn;
    public int numOfCarrot;
    public int numOfCabbage;
    public int numOfOnion;
}

[System.Serializable]
public class SerializableDataWrapper
{
    public List<SerializableLandCellArray> data;
}


