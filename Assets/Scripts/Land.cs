using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using TMPro;
public class Land : MonoBehaviour
{
    public bool isPanted = false;
    public int landID;
    public string landPantedType;
    public float water;
    public float sun;
    

    // Start is called before the first frame update
    void Start()
    {
        water = RandomResources.GetRandom();
        sun = RandomResources.GetRandom();
        landID = FindID();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isPanted) return;
        if (other.gameObject.tag == "SeedPack")
        {
            string seedType = other.gameObject.GetComponent<SeedPack>().seedType;
            landPantedType = seedType;
            Planting(seedType);
            isPanted = true;
        }
    }

    void Planting(string seedType)
    {
        Instantiate(Resources.Load($"Prefabs/Plant/{seedType}"), transform.position, Quaternion.identity, transform);
        //Debug.Log("Planting " + seedType);
    }

    private void OnMouseDown()
    {
        if (PlantManager.Instance.packSelected && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        UIManager.Instance.panel.SetActive(true);
        UIManager.Instance.setLand(gameObject);
    }

    public void NextTurn()
    {
        water += RandomResources.GetRandom();
        sun = RandomResources.GetRandom();
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
}
