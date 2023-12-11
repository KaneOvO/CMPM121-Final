using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using TMPro;
using System;
public class Land : MonoBehaviour
{
    public float sun;

    // Start is called before the first frame update
    void Start()
    {
        sun = GetSun();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (PlantManager.landArea.GetLandCell(FindID()).isPanted) return;
        GameManager.Instance.SaveCureentSituations();
        if (other.gameObject.tag == "SeedPack")
        {
            PlantType seedType = other.gameObject.GetComponent<SeedPack>().seedType;
            PlantManager.landArea.GetLandCell(FindID()).landPlantedType = seedType;
            Planting(seedType);
            PlantManager.landArea.GetLandCell(FindID()).isPanted = true;
            GameManager.Instance.ClearRedoStack();
        }

    }

    void Planting(PlantType seedType)
    {
        string plantType = seedType switch
        {
            PlantType.CARROT => "Carrot",
            PlantType.CABBAGE => "Cabbage",
            PlantType.ONION => "Onion",
            PlantType.EMPTY => "",
            _ => ""

        };
        //Debug.Log(seedType);
        if (plantType == "") return;
        Instantiate(Resources.Load($"Prefabs/Plant/{plantType}"), transform.position, Quaternion.identity, transform);

    }

    private void OnMouseDown()
    {
        if (PlantManager.Instance.packSelected && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        UIManager.Instance.panel.SetActive(true);
        UIManager.Instance.setLand(gameObject);
        PlantManager.Instance.setLand(gameObject);
    }

    // public void NextTurn()
    // {
    //     Growable growable = GetComponentInChildren<Growable>();
    //     if (growable != null)
    //     {
    //         LandCell currentCell = PlantManager.landArea.GetLandCell(FindID());
    //         PlantType plantType = currentCell.landPlantedType;
    //         int currentStage = currentCell.currentStage;

    //         if (PlantDefinition.Plants.TryGetValue(plantType, out var plantStages) &&
    //         currentStage < plantStages.Count)
    //         {
    //             Plant currentPlant = plantStages[currentStage];
    //             GrowthContext context = new GrowthContext(currentCell.water, sun, PlantManager.landArea.GetLandCell(FindID() - 1).isPanted , PlantManager.landArea.GetLandCell(FindID() + 1).isPanted);


    //             if (currentPlant.CheckGrowth(context))
    //             {
    //                 currentCell.water -= currentPlant.consumingWater;
    //                 currentCell.currentStage++;
    //                 PlantManager.landArea.GetLandCell(FindID()).currentStage = currentCell.currentStage;
    //                 growable.setStage(currentCell.currentStage);
    //             }
    //         }

    //     }
    //     PlantManager.landArea.GetLandCell(FindID()).water += RandomResources.GetRandom();
    //     sun = GetSun();
    // }

    public void NextTurn()
    {
        Growable growable = GetComponentInChildren<Growable>();
        if (growable != null)
        {
            int totalColumns = 9;
            int index = FindID();
            int row = index / totalColumns;
            int column = index % totalColumns; 

            LandCell currentCell = PlantManager.landArea.GetLandCell(index);
            PlantType plantType = currentCell.landPlantedType;
            int currentStage = currentCell.currentStage;

            bool leftIsPlanted = false;
            bool rightIsPlanted = false;

            if (column > 0)
            {
                leftIsPlanted = PlantManager.landArea.GetLandCell(index - 1).isPanted;
            }

            if (column < totalColumns - 1)
            {
                rightIsPlanted = PlantManager.landArea.GetLandCell(index + 1).isPanted;
            }

            if (PlantDefinition.Plants.TryGetValue(plantType, out var plantStages) && currentStage < plantStages.Count)
            {
                Plant currentPlant = plantStages[currentStage];
                GrowthContext context = new GrowthContext(currentCell.water, sun, leftIsPlanted, rightIsPlanted);

                if (currentPlant.CheckGrowth(context))
                {
                    currentCell.water -= currentPlant.consumingWater;
                    currentCell.currentStage++;
                    PlantManager.landArea.GetLandCell(index).currentStage = currentCell.currentStage;
                    growable.setStage(currentCell.currentStage);
                }
            }

            PlantManager.landArea.GetLandCell(index).water += RandomResources.GetRandom();
            sun = GetSun();
        }
    }

    public void undoThisLand()
    {
        PlantManager.landArea.landCells[FindID()] = GameManager.undoData.landArea.landCells[FindID()];
        sun = GetSun();

        Growable growable = GetComponentInChildren<Growable>();

        if (PlantManager.landArea.GetLandCell(FindID()).isPanted == false)
        {
            if (growable != null)
            {
                Debug.Log(growable);
                Destroy(GetComponentInChildren<Growable>().gameObject);
            }

        }
        else
        {
            if (growable != null)
            {
                growable.setStage(PlantManager.landArea.GetLandCell(FindID()).currentStage);

            }
            else
            {
                Planting(PlantManager.landArea.GetLandCell(FindID()).landPlantedType);
                growable = GetComponentInChildren<Growable>();
                growable.setStage(PlantManager.landArea.GetLandCell(FindID()).currentStage);
            }
        }



    }

    public void redoThisLand()
    {
        PlantManager.landArea.landCells[FindID()] = GameManager.redoData.landArea.landCells[FindID()];
        sun = GetSun();

        Growable growable = GetComponentInChildren<Growable>();

        if (PlantManager.landArea.GetLandCell(FindID()).isPanted == false)
        {
            if (growable != null)
            {
                Destroy(GetComponentInChildren<Growable>().gameObject);
            }

        }
        else
        {
            if (growable != null)
            {

                growable.setStage(PlantManager.landArea.GetLandCell(FindID()).currentStage);

            }
            else
            {
                Planting(PlantManager.landArea.GetLandCell(FindID()).landPlantedType);
                growable = GetComponentInChildren<Growable>();
                if (growable != null)
                {
                    growable.setStage(PlantManager.landArea.GetLandCell(FindID()).currentStage);

                }
                else
                {
                    Debug.Log("Growable is null");
                };

            }
        }


    }

    public int FindID()
    {
        string gameObjectName = gameObject.name;
        string pattern = @"\((\d+)\)";
        Match match = Regex.Match(gameObjectName, pattern);
        string number = "";
        if (match.Success)
        {
            number = match.Groups[1].Value;
        }

        return int.Parse(number);
    }

    public int FindID(GameObject gameObject)
    {
        string gameObjectName = gameObject.name;
        string pattern = @"\((\d+)\)";
        Match match = Regex.Match(gameObjectName, pattern);
        string number = "";
        if (match.Success)
        {
            number = match.Groups[1].Value;
        }

        return int.Parse(number);
    }
    public float GetSun()
    {
        int index = FindID();
        float water = PlantManager.landArea.GetLandCell(index).water;
        int currentTurn = GameManager.Instance.currentTurn;
        float sunValue = Mathf.PerlinNoise(index * GlobalValue.RANDOM_FACTOR3, currentTurn * GlobalValue.RANDOM_FACTOR1) * GlobalValue.RANDOM_FACTOR_FIFTY +
            Mathf.PerlinNoise(water * GlobalValue.RANDOM_FACTOR1, currentTurn * GlobalValue.RANDOM_FACTOR2) * GlobalValue.RANDOM_FACTOR_FIFTY;
        return Mathf.Clamp(sunValue, GlobalValue.RANDOM_FLOOR, GlobalValue.RANDOM_CEIL);
    }

    public void loadThisLand()
    {
        sun = GetSun();

        Growable growable = GetComponentInChildren<Growable>();

        if (PlantManager.landArea.GetLandCell(FindID()).isPanted == false)
        {
            if (growable != null)
            {
                Destroy(GetComponentInChildren<Growable>().gameObject);
            }

        }
        else
        {
            //Debug.Log(PlantManager.landArea.GetLandCell(FindID()).currentStage);
            if (growable != null)
            {

                growable.setStage(PlantManager.landArea.GetLandCell(FindID()).currentStage);
                //Debug.Log("growable != null" + growable.currentStage);

            }
            else
            {
                Planting(PlantManager.landArea.GetLandCell(FindID()).landPlantedType);
                growable = GetComponentInChildren<Growable>();

                if (growable != null)
                {
                    //Debug.Log("Current Stage " + PlantManager.landArea.GetLandCell(FindID()).currentStage);
                    growable.setStage(PlantManager.landArea.GetLandCell(FindID()).currentStage);
                    //Debug.Log("growable == null " + growable.currentStage);
                }
                else
                {
                    //Debug.Log("Growable is null");
                };

            }

        }
    }
}
