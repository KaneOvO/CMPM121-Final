using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomResources : MonoBehaviour
{

    public static float GetRandom()
    {
        return Random.Range(0, 100f);
    }

}