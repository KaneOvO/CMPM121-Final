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
        Debug.Log(seedType);
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
            if (stage < 3 && sun > stage * 10f && PlantManager.landArea.GetLandCell(FindID()).water > stage * 25f)
            {
                PlantManager.landArea.GetLandCell(FindID()).water -= PlantManager.landArea.GetLandCell(FindID()).currentStage * 25;
                PlantManager.landArea.GetLandCell(FindID()).currentStage++;
                growable.setStage(stage);
            }

        }

        PlantManager.landArea.GetLandCell(FindID()).water += RandomResources.GetRandom();
        sun = GetSun();
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
        float sunValue = Mathf.PerlinNoise(index * 0.5f, currentTurn * 0.1f) * 50f +
            Mathf.PerlinNoise(water * 0.1f, currentTurn * 0.2f) * 50f;
        return Mathf.Clamp(sunValue, 0f, 100f);
    }
}
