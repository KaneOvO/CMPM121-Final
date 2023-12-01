using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class Land : MonoBehaviour
{
    public bool isPanted = false;
    public float water;
    public float sun;

    public TextMeshProUGUI waterText;
    public TextMeshProUGUI sunText;


    // Start is called before the first frame update
    void Start()
    {
        water = RandomResources.GetRandom();
        sun = RandomResources.GetRandom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isPanted) return;
        if(other.gameObject.tag == "SeedPack")
        {
            string seedType = other.gameObject.GetComponent<SeedPack>().seedType;
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
        if(PlantManager.Instance.packSelected && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        UIManager.Instance.panel.SetActive(true);
        waterText.text = "= " + water.ToString("0");
        sunText.text = "= " + sun.ToString("0");    
          
    }

    public void NextTurn()
    {
        water += RandomResources.GetRandom();
        sun = RandomResources.GetRandom();
        //Debug.Log("Water: " + water + " Sun: " + sun);
    }
}
