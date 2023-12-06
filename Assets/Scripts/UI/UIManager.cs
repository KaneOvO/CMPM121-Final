using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject panel;

    public TextMeshProUGUI waterText;
    public TextMeshProUGUI sunText;
    public TextMeshProUGUI carrotText;
    public TextMeshProUGUI cabbageText;
    public TextMeshProUGUI OnionText;
    public GameObject endText;
    private GameObject land;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(land != null)
        {
            ChangeWaterText();
            ChangeSunText();
        }
        
        if(PlantManager.Instance.numOfCarrot >= 5 && PlantManager.Instance.numOfCabbage >= 5 && PlantManager.Instance.numOfOnion >= 5)
        {
            endText.SetActive(true);
        }
    }

    private void ChangeWaterText()
    {
        waterText.text = "= " + PlantManager.landArea.GetLandCell(land.GetComponent<Land>().FindID()).water.ToString("0");
    }

    private void ChangeSunText()
    {
        sunText.text = "= " + land.GetComponent<Land>().sun.ToString("0");
    }

    public void ChangeCarrotText(int carrot)
    {
        carrotText.text = "= " + carrot.ToString();
    }

    public void ChangeCabbageText(int cabbage)
    {
        cabbageText.text = "= " + cabbage.ToString();
    }

    public void ChangeOnionText(int onion)
    {
        OnionText.text = "= " + onion.ToString();
    }
     
    public void setLand(GameObject land)
    {
        this.land = land;
    }

}
