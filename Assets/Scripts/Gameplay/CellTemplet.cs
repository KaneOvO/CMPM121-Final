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