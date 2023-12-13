using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class GameSettings
{
    public Scenario[] scenarios;
}

[System.Serializable]
public class Scenario
{
    public string name;
    public ScenarioSettings settings;
}

[System.Serializable]
public class ScenarioSettings
{
    public int maxTurns;
    public string humanInstructions;
    public WinCondition[] winConditions;
}

[System.Serializable]
public class WinCondition
{
    public string condition;
    public int number;
}


public class testReadScenario : MonoBehaviour
{
    public event Action<GameSettings> OnJsonLoaded;
    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "example.json");
        StartCoroutine(ReadJsonFile(filePath));
    }

    IEnumerator ReadJsonFile(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to load file: {uwr.error}");
        }
        else
        {
            try
            {
                string jsonContent = uwr.downloadHandler.text;
                GameSettings gameSettings = JsonUtility.FromJson<GameSettings>(jsonContent);
                ProcessGameSettings(gameSettings);
                OnJsonLoaded?.Invoke(gameSettings);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error processing JSON file: {ex.Message}");
            }
        }
    }

    private void ProcessGameSettings(GameSettings gameSettings)
    {
        foreach (Scenario scenario in gameSettings.scenarios)
        {
            GameManager.Instance.maxTurns = scenario.settings.maxTurns;
            // GameManager.Instance.humanInstructions = scenario.settings.humanInstructions;
            foreach (WinCondition winCondition in scenario.settings.winConditions)
            {
                switch (winCondition.condition)
                {
                    case "Carrot":
                        GameManager.Instance.carrotNeeded = winCondition.number;
                        break;
                    case "Cabbage":
                        GameManager.Instance.cabbageNeeded = winCondition.number;
                        break;
                    case "Onion":
                        GameManager.Instance.onionNeeded = winCondition.number;
                        break;
                    default:
                        Debug.LogError($"No match found for condition: {winCondition.condition}");
                        break;
                }
            }
            // Debug.Log(GameManager.Instance.maxTurns);
            // Debug.Log(GameManager.Instance.humanInstructions);
            // Debug.Log(GameManager.Instance.carrotNeeded);
        }
    }
}
