using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandArea : MonoBehaviour
{
    public LandCell[] landCells = new LandCell[GlobalValue.LAND_NUM];
    private int index = 0;

    public LandCell GetLandCell(int i) {
        LandCell landCell = landCells[i];
        if (landCell != null) return landCell;

        byte[] landBuffer = new byte[GlobalValue.LAND_NUM * LandCell.NumBytes];
        return new LandCell(landBuffer, i * LandCell.NumBytes)
            {
                isPanted = false,
                landPlantedType = PlantType.EMPTY,
                currentStage = 0,
                water = RandomResources.GetRandom(),
            };
    }

    public void addCell(LandCell landCell) {
        Debug.Log(landCell);
        Debug.Log(index);
        landCells[index] = landCell;
        if (index < GlobalValue.LAND_NUM) {
            index++;
        }
    }

    public void removeCell() {
        byte[] landBuffer = new byte[GlobalValue.LAND_NUM * LandCell.NumBytes];
        landCells[index] = new LandCell(landBuffer, index * LandCell.NumBytes)
            {
                isPanted = false,
                landPlantedType = PlantType.EMPTY,
                currentStage = 0,
                water = RandomResources.GetRandom(),
            };
        if (index != 0) {
            index --;
        }
    }

    public void removeCell(int i) {
        byte[] landBuffer = new byte[GlobalValue.LAND_NUM * LandCell.NumBytes];
        landCells[i] = new LandCell(landBuffer, i * LandCell.NumBytes)
            {
                isPanted = false,
                landPlantedType = PlantType.EMPTY,
                currentStage = 0,
                water = RandomResources.GetRandom(),
            };
    }

    public void CallNextTurnOnChildren()
    {
        foreach (Transform child in transform)
        {
            Land land = child.GetComponent<Land>();
            if (land != null)
            {
                land.NextTurn();
            }
        }
    }
    
}
