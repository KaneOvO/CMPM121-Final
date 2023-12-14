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
    public TextMeshProUGUI InstructionText;
    public GameObject winText;
    public GameObject loseText;
    private GameObject land;
    public GameObject saveDataPanal;
    private bool isInitializedExternalDSL = false;
    private bool isShowInstruction = false;

    private bool isInitializedLanguage = false;
    private bool isLoadSetting = false;
    private bool isTimeShowPanal = true;

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
        FindObjectOfType<testReadScenario>().OnJsonLoaded += OnJsonLoaded;
        FindObjectOfType<SetLanguage>().OnJsonLoaded += OnJsonLoaded;
        Invoke("SetIsTimeShowPanel", 1f);
    }

    private void Update()
    {
        if (!isLoadSetting)
        {
            if (File.Exists(Application.persistentDataPath + "/landAreaSaveAuto_undo.json") && File.Exists(Application.persistentDataPath + "/landAreaSaveAuto_redo.json"))
            {
                if (isInitializedLanguage)
                {
                    isLoadSetting = true;
                    string un_json = File.ReadAllText(Application.persistentDataPath + "/landAreaSaveAuto_undo.json");
                    SerializableDataWrapper un_wrapper = JsonUtility.FromJson<SerializableDataWrapper>(un_json);
                    if (un_wrapper.language != FindObjectOfType<SetLanguage>().currentLanguage)
                    {
                        FindObjectOfType<SetLanguage>().updateLanguage(un_wrapper.language);
                    }
                    
                }
                if(isTimeShowPanal)
                {
                    saveDataPanal.SetActive(true);
                }
            }
        }

        if (land != null)
        {
            ChangeWaterText();
            ChangeSunText();
        }

        if (isInitializedExternalDSL)
        {
            if (PlantManager.Instance.numOfCarrot >= GameManager.Instance.carrotNeeded &&
         PlantManager.Instance.numOfCabbage >= GameManager.Instance.cabbageNeeded &&
          PlantManager.Instance.numOfOnion >= GameManager.Instance.onionNeeded)
            {
                winText.SetActive(true);
            }

            if (!isShowInstruction)
            {
                FindObjectOfType<SetLanguage>().updateInstruction();
                isShowInstruction = true;
            }

        }


        if (File.Exists(Application.persistentDataPath + "/landAreaSaveAuto_undo.json") || File.Exists(Application.persistentDataPath + "/landAreaSaveAuto_redo.json"))
        {
            if (isInitializedLanguage)
            {
                SaveDataAutoText.text = FindObjectOfType<SetLanguage>().loadedData.SavedataAuto_on[FindObjectOfType<SetLanguage>().currentLanguage];
            }
        }


        if (File.Exists(Application.persistentDataPath + "/landAreaSave1_undo.json") || File.Exists(Application.persistentDataPath + "/landAreaSave1_redo.json"))
        {
            if (isInitializedLanguage)
            {
                SaveData1Text.text = FindObjectOfType<SetLanguage>().loadedData.Savedata1_on[FindObjectOfType<SetLanguage>().currentLanguage];
            }
        }


        if (File.Exists(Application.persistentDataPath + "/landAreaSave2_undo.json") || File.Exists(Application.persistentDataPath + "/landAreaSave2_undo.json"))
        {
            if (isInitializedLanguage)
            {
                SaveData2Text.text = FindObjectOfType<SetLanguage>().loadedData.Savedata2_on[FindObjectOfType<SetLanguage>().currentLanguage];
            }
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

    public void ChangeText()
    {
        ChangeCabbageText();
        ChangeCarrotText();
        ChangeOnionText();
    }
    public void TurnOnSaveDataPanal()
    {
        saveDataPanal.SetActive(true);
    }
    public void LoadSaveDataClick()
    {
        //SavaDataManager.Instance.LoadSaveData();

        saveDataPanal.SetActive(false);
    }

    public void QuitSaveDataClick()
    {
        saveDataPanal.SetActive(false);
    }
    public void SaveSaveDataClick()
    {
        saveDataPanal.SetActive(false);
    }

    private void OnJsonLoaded(GameSettings settings)
    {
        isInitializedExternalDSL = true;
    }

    private void OnJsonLoaded(SetLanguage language)
    {
        isInitializedLanguage = true;
    }

    private void SetIsTimeShowPanel()
    {
        this.isTimeShowPanal = false;
    }


}
