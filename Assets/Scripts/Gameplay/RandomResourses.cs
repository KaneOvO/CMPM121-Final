using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomResources : MonoBehaviour
{

    public void GetWater(Cell cell)
    {
        cell.water += GetRandom();
    }
    public static float GetSun(Cell cell)
    {
        // Pseudo-random sun value based on the cell's position, water, and turn count
        float sunValue = CalculateSunValue(cell.pos, cell.water, GameManager.Instance.currentTurn);

        // Modify the sun value further if needed

        return sunValue;
    }

    private static float CalculateSunValue(Vector3 position, float water, int currentTurn)
    {
        float sunValue = Mathf.PerlinNoise(position.x * 0.2f, position.y * 0.1f) * 50f +
                          Mathf.PerlinNoise(water * 0.1f, currentTurn * 0.2f) * 50f;
        return Mathf.Clamp(sunValue, 0f, 100f);
    }

    public static float GetRandom()
    {
        return Random.Range(0, 100f);
    }
}
