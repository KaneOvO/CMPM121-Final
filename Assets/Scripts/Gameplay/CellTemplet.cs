using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cell
{
    public Vector3 pos;
    public float water;
    public bool isLand;
    public plantType plant;
    public int level;

    public float getSun()
    {
        float sun = pos.x * pos.y * GameManager.Instance.currentTurn;
        return sun;
    }
    public void grow(float growUp)
    {
        if (water * getSun() * nearByPlants() >= growUp)
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