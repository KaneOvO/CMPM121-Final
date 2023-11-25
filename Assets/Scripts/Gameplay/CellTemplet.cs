using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cell
{
    public Vector3 pos;
    public float water;
    public bool isLand;
    public plantType plant;
    public int level;
    public Cell(Vector3 pos, float water, bool isLand, plantType plant, int level)
    {
        this.pos = pos;
        this.water = water;
        this.isLand = isLand;
        this.plant = plant;
        this.level = level;
    }
    public void grow(float growUp)
    {
        if (water * RandomResources.GetSun(this) * nearByPlants() >= growUp)
        {
            level++;
        }
    }

    public float nearByPlants()
    {
        return 1;
    }
}

public enum plantType
{
    None,
    Grass,
    Tree
}