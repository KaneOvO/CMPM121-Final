using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject panel;

    public TextMeshProUGUI waterText;
    public TextMeshProUGUI sunText;
    public TextMeshProUGUI carrotText;
    public TextMeshProUGUI cabbageText;
    public TextMeshProUGUI OnionText;
    public TextMeshProUGUI SaveDataAutoText;
    public TextMeshProUGUI SaveData1Text;
    public TextMeshProUGUI SaveData2Text;
    public GameObject endText;
    private GameObject land;
    public GameObject saveDataPanal;
    public int carrotNeeded;
    public int cabbageNeeded;
    public int onionNeeded;

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

    private void Start()
    {
        if(File.Exists(Application.persistentDataPath + "/landAreaSaveAuto_undo.json") && File.Exists(Application.persistentDataPath + "/landAreaSaveAuto_redo.json"))
        {
            saveDataPanal.SetActive(true);
        }
    }

    private void Update()
    {
        if (land != null)
        {
            ChangeWaterText();
            ChangeSunText();
        }

        if (PlantManager.Instance.numOfCarrot >= carrotNeeded &&
         PlantManager.Instance.numOfCabbage >= cabbageNeeded &&
          PlantManager.Instance.numOfOnion >= onionNeeded)
        {
            endText.SetActive(true);
        }

        if(File.Exists(Application.persistentDataPath + "/landAreaSaveAuto_undo.json") && File.Exists(Application.persistentDataPath + "/landAreaSaveAuto_redo.json"))
        {
            SaveDataAutoText.text = "Auto Save Data";
        }


        if (File.Exists(Application.persistentDataPath + "/landAreaSave1_undo.json") && File.Exists(Application.persistentDataPath + "/landAreaSave1_redo.json"))
        {
            SaveData1Text.text = "Save Data 1";
        }
        

        if (File.Exists(Application.persistentDataPath + "/landAreaSave2_undo.json") && File.Exists(Application.persistentDataPath + "/landAreaSave2_undo.json"))
        {
            SaveData2Text.text = "Save Data 2";
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
