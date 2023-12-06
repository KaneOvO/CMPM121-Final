// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ForTest : MonoBehaviour
// {
//     public List<Cell> myCells = new List<Cell>(); // Initialize myCells as a list

//     // Start is called before the first frame update

//     void Start()
//     {
//         Test();
//     }
//     public void Test()
//     {
//         for (int i = 0; i < 10; i++)
//         {
//             myCells.Add(new Cell(new Vector3(1, 2, 3), 4, true, plantType.Grass, 1));
//         }

//         myCells.ForEach(cell =>
//         {
//             Debug.Log(GameManager.Instance.currentTurn);
//             Debug.Log(GetSun(cell));
//         });
//         myCells = new List<Cell>();
//     }

//     public static float GetSun(Cell cell)
//     {
//         // Pseudo-random sun value based on the cell's position, water, and turn count
//         float sunValue = CalculateSunValue(cell.pos, cell.water, GameManager.Instance.currentTurn);

//         // Modify the sun value further if needed

//         return sunValue;
//     }

//     private static float CalculateSunValue(Vector3 position, float water, int currentTurn)
//     {
//         float sunValue = Mathf.PerlinNoise(position.x * 0.2f, position.y * 0.1f) * 50f +
//                           Mathf.PerlinNoise(water * 0.1f, currentTurn * 0.2f) * 50f;
//         return Mathf.Clamp(sunValue, 0f, 100f);
//     }

// }
