// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor.U2D.Aseprite;
// using UnityEngine;
// using UnityEngine.Tilemaps;

// public class PlantManager : MonoBehaviour
// {
//     public static PlantManager Instance { get; private set; }
//     public int plantSelected;
//     public int numOfCarrot;
//     public int numOfCabbage;
//     public int numOfOnion;

//     public Tilemap landTilemap;
//     public Tilemap growingZoneTilemap;

//     void Awake()
//     {

//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }


//     // Start is called before the first frame update
//     void Start()
//     {
//         plantSelected = 0;
//         numOfCarrot = 0;
//         numOfCabbage = 0;
//         numOfOnion = 0;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (plantSelected != 0)
//         {
//             Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             Vector3Int cellPos = landTilemap.WorldToCell(worldPos);

//             if (CanPlantSeed(cellPos))
//             {
//                 PlantSeed(cellPos);
//             }
//         }
//     }

//     bool CanPlantSeed(Vector3Int position)
//     {
//         bool landTileExists = landTilemap.GetTile(position) != null;
//         bool growingZoneTileExists = growingZoneTilemap.GetTile(position) != null;

//         return landTileExists && !growingZoneTileExists;
//     }

//     void PlantSeed(Vector3Int position)
//     {
//         Debug.Log("Planting seed at " + position);
//         Debug.Log("Planting seed type " + plantSelected);
//         switch (plantSelected)
//         {
//             case 1:
//                 growingZoneTilemap.SetTile(position, Resources.Load<Tile>("Tiles/Carrot"));
//                 break;
//             case 2:
//                 growingZoneTilemap.SetTile(position, Resources.Load<Tile>("Tiles/Cabbage"));
//                 break;
//             case 3:
//                 growingZoneTilemap.SetTile(position, Resources.Load<Tile>("Tiles/Onion"));
//                 break;
//         }
//     }

//     void plantInformation(Vector3Int position )
//     {
        
//     }
// }
