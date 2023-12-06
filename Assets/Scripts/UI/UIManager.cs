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
    public GameObject saveDataPanal;

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
        if (land != null)
        {
            ChangeWaterText();
            ChangeSunText();
        }

        if (PlantManager.Instance.numOfCarrot >= GlobalValue.END_GAME_CONDITION &&
         PlantManager.Instance.numOfCabbage >= GlobalValue.END_GAME_CONDITION &&
          PlantManager.Instance.numOfOnion >= GlobalValue.END_GAME_CONDITION)
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

    public void ChangeCarrotText()
    {
        carrotText.text = "= " + PlantManager.Instance.numOfCarrot.ToString();
    }

    public void ChangeCabbageText()
    {
        cabbageText.text = "= " + PlantManager.Instance.numOfCabbage.ToString();
    }

    public void ChangeOnionText()
    {
        OnionText.text = "= " + PlantManager.Instance.numOfOnion.ToString();
    }

    

    public void setLand(GameObject land)
    {
        this.land = land;
    }

    public void ChangeText(){
        ChangeCabbageText();
        ChangeCarrotText();
        ChangeOnionText();
    }
    public void TurnOnSaveDataPanal(){
        saveDataPanal.SetActive(true);
    }
    public void LoadSaveDataClick(){
        //SavaDataManager.Instance.LoadSaveData();
        saveDataPanal.SetActive(false);
    }

    public void QuitSaveDataClick(){
        saveDataPanal.SetActive(false);
    }
    public void SaveSaveDataClick(){
        //SaveDataManager.Instance.SaveSaveData();
        saveDataPanal.SetActive(false);
    }
}
