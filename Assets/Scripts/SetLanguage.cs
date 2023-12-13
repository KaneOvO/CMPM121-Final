using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Reflection;
using UnityEngine.Networking;

public class SetLanguage : MonoBehaviour
{
    public event Action<SetLanguage> OnJsonLoaded;
    public LocalizationData loadedData;
    TMP_FontAsset textAsset;
    public int currentLanguage = GlobalValue.ENGLISH_LANGUAGE_INDEX;
    // void Start()
    // {
    //     string filePath = Path.Combine(Application.streamingAssetsPath, "language_setting.json");
    //     if (File.Exists(filePath))
    //     {
    //         string dataAsJson = File.ReadAllText(filePath);
    //         loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

    //     }
    // }

    IEnumerator Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "language_setting.json");

        using (UnityWebRequest webRequest = UnityWebRequest.Get(filePath))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string dataAsJson = webRequest.downloadHandler.text;
                loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
                OnJsonLoaded?.Invoke(this);
            }
        }
    }
    void FindAndChangeAllText()
    {
        GameObject parentObject = GameObject.Find("UICanvas");

        if (parentObject != null)
        {
            updateDate();
            updateInstruction();
            FindAndChangeAllTextChildren(parentObject.transform);
        }
        else
        {
            Debug.Log("Root GameObject not found.");
        }
    }

    public List<string> GetListByName(LocalizationData data, string listName)
    {
        Type type = data.GetType();
        FieldInfo fieldInfo = type.GetField(listName);
        if (fieldInfo != null && fieldInfo.FieldType == typeof(List<string>))
        {
            return (List<string>)fieldInfo.GetValue(data);
        }
        return null;
    }
    void FindAndChangeAllTextChildren(Transform parent)
    {

        foreach (Transform child in parent)
        {
            if (child.parent.name == "SettingsPanel" || child.parent.name == "Bg" || child.parent.name == "Instruction") continue;
            if (child.name == "Text (TMP)")
            {
                TextMeshProUGUI tmpComponent = child.GetComponent<TextMeshProUGUI>();
                ArabicFixerTMPRO arabicFixerTMPRO = child.GetComponent<ArabicFixerTMPRO>() ?? child.gameObject.AddComponent<ArabicFixerTMPRO>();
                arabicFixerTMPRO.enabled = currentLanguage == GlobalValue.ARABIC_LANGUAGE_INDEX ? true : false;
                if (tmpComponent != null)
                {

                    tmpComponent.GetComponent<TextMeshProUGUI>().font = textAsset;
                    List<string> languageList = GetListByName(loadedData, child.parent.name);
                    if (languageList != null)
                    {
                        tmpComponent.text = GetListByName(loadedData, child.parent.name)[currentLanguage];
                        if (currentLanguage == GlobalValue.ARABIC_LANGUAGE_INDEX)
                        {
                            arabicFixerTMPRO.fixedText = GetListByName(loadedData, child.parent.name)[currentLanguage];
                        }

                    }
                }
            }
            FindAndChangeAllTextChildren(child);
        }
    }
    public void updateLanguage(int languageIndex)
    {
        updateInstruction();
        if (currentLanguage != languageIndex)
        {
            currentLanguage = languageIndex;

            switch (currentLanguage)
            {
                case GlobalValue.ENGLISH_LANGUAGE_INDEX:
                    setEnglish();
                    break;
                case GlobalValue.CHINESE_LANGUAGE_INDEX:
                    setChinese();
                    break;
                case GlobalValue.ARABIC_LANGUAGE_INDEX:
                    setArabic();
                    break;
            }
        }
    }
    public void updateDate()
    {
        GameObject.Find("Bg").transform.GetChild(0).GetComponent<TextMeshProUGUI>().font = textAsset;
        switch (currentLanguage)
        {
            case GlobalValue.ARABIC_LANGUAGE_INDEX:
                GameObject.Find("Bg").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.currentTurn + loadedData.Day[currentLanguage];
                break;
            default:
                GameObject.Find("Bg").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = loadedData.Day[currentLanguage] + GameManager.Instance.currentTurn;
                break;
        }

    }
    public void updateInstruction()
    {
        GameObject.Find("Instruction").transform.GetChild(0).GetComponent<TextMeshProUGUI>().font = textAsset;
        switch (currentLanguage)
        {
            case GlobalValue.ENGLISH_LANGUAGE_INDEX:
                GameObject.Find("Instruction").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Grow at least:" + (GameManager.Instance.carrotNeeded > 0 ? $" {GameManager.Instance.carrotNeeded} carrots " : "") + (GameManager.Instance.cabbageNeeded > 0 ? $" {GameManager.Instance.cabbageNeeded} cabbage " : "") + (GameManager.Instance.onionNeeded > 0 ? $" {GameManager.Instance.onionNeeded} onion," : "") + $" in {GameManager.Instance.maxTurns} turns";
                break;
            case GlobalValue.CHINESE_LANGUAGE_INDEX:
                GameObject.Find("Instruction").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"在 {GameManager.Instance.maxTurns} 轮内种植至少:" + (GameManager.Instance.carrotNeeded > 0 ? $" {GameManager.Instance.carrotNeeded} 个胡萝卜 " : "") + (GameManager.Instance.cabbageNeeded > 0 ? $" {GameManager.Instance.cabbageNeeded} 个卷心菜 " : "") + (GameManager.Instance.onionNeeded > 0 ? $" {GameManager.Instance.onionNeeded} 个洋葱 " : "");
                break;
            case GlobalValue.ARABIC_LANGUAGE_INDEX:
                ArabicFixerTMPRO arabicFixerTMPRO = GameObject.Find("Instruction").transform.GetChild(0).GetComponent<ArabicFixerTMPRO>() ?? GameObject.Find("Instruction").transform.GetChild(0).gameObject.AddComponent<ArabicFixerTMPRO>();
                string arabicInstruction = "قم بزراعة ما لا يقل عن:" + (GameManager.Instance.carrotNeeded > 0 ? $" {GameManager.Instance.carrotNeeded} جزرات " : "") + (GameManager.Instance.cabbageNeeded > 0 ? $" {GameManager.Instance.cabbageNeeded} حبات ملفوف " : "") + (GameManager.Instance.onionNeeded > 0 ? $" {GameManager.Instance.onionNeeded} بصلة، " : "") + $" في {GameManager.Instance.maxTurns} دورات";
                GameObject.Find("Instruction").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = arabicInstruction;
                arabicFixerTMPRO.fixedText = arabicInstruction;
                break;
        }
    }
    public void setArabic()
    {
        textAsset = Resources.Load<TMP_FontAsset>("Font/Tajawal-Light SDF");
        currentLanguage = GlobalValue.ARABIC_LANGUAGE_INDEX;
        FindAndChangeAllText();
    }
    public void setChinese()
    {
        textAsset = Resources.Load<TMP_FontAsset>("Font/NotoSansSC-Regular SDF");
        currentLanguage = GlobalValue.CHINESE_LANGUAGE_INDEX;
        FindAndChangeAllText();
    }
    public void setEnglish()
    {
        textAsset = Resources.Load<TMP_FontAsset>("Font/LiberationSans SDF");
        currentLanguage = GlobalValue.ENGLISH_LANGUAGE_INDEX;
        FindAndChangeAllText();
    }



    void Update()
    {

    }
}

[System.Serializable]
public class LocalizationData
{
    public List<string> Day;
    public List<string> SaveButton;
    public List<string> RedoButton;
    public List<string> UndoButton;
    public List<string> NextTurn;
    public List<string> Gather;
    public List<string> Close;
    public List<string> SavedataAuto;
    public List<string> SavedataAuto_on;
    public List<string> Savedata1;
    public List<string> Savedata1_on;
    public List<string> Savedata2;
    public List<string> Savedata2_on;
    public List<string> Load;
    public List<string> Save;
    public List<string> Exit;
    public List<string> GameOver;
    public List<string> GameWin;
    public List<string> StartSavePanel;
    public List<string> SettingButton;
    public List<string> Yes;
    public List<string> No;
    public List<string> Instruction;

}

