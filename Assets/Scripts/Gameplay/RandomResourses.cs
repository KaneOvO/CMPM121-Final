using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomResources : MonoBehaviour
{

    public void GetWater(Cell cell){
        cell.water += GetRandom();
    }
    public static float GetSun(Cell cell){
        //TODO:persudo random, generate sun value based on teh cell's data, modified it later
        return GetRandom();
    }
    public static float GetRandom(){
        return Random.Range(0,100f);
    }
}
