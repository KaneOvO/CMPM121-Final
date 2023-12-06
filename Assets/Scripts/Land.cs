using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using TMPro;
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
        if (other.gameObject.tag == "SeedPack")
        {
            PlantType seedType = other.gameObject.GetComponent<SeedPack>().seedType;
            PlantManager.landArea.GetLandCell(FindID()).landPlantedType = seedType;
            Planting(seedType);
            PlantManager.landArea.GetLandCell(FindID()).isPanted = true;
            GameManager.Instance.SaveCureentSituations();
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

    public void NextTurn()
    {
        Growable growable = GetComponentInChildren<Growable>();
        if (growable != null)
        {
            //Debug.Log("Water: " + PlantManager.landCells[FindID()].water + " Sun: " + sun);
            int stage = PlantManager.landArea.GetLandCell(FindID()).currentStage + 1;
            if (stage <= GlobalValue.MAX_STAGE &&
                sun > stage * GlobalValue.SUNSHINE_LEVEL_RANGE &&
                PlantManager.landArea.GetLandCell(FindID()).water > stage * GlobalValue.WATER_LEVEL_RANGE)
            {
                PlantManager.landArea.GetLandCell(FindID()).water -= PlantManager.landArea.GetLandCell(FindID()).currentStage * GlobalValue.WATER_LEVEL_RANGE;
                PlantManager.landArea.GetLandCell(FindID()).currentStage++;
                growable.setStage(stage);
            }

        }

        PlantManager.landArea.GetLandCell(FindID()).water += RandomResources.GetRandom();
        sun = GetSun();
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
}
