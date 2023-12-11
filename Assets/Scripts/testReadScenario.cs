using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

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
    public static GameSettings ReadJsonFile(string filePath)
    {
        try
        {
            string jsonContent = File.ReadAllText(filePath);
            return JsonUtility.FromJson<GameSettings>(jsonContent);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading JSON file: {ex.Message}", ex);
        }
    }


    void Start()
    {
        try
        {
            string filePath = "Assets/example.json";
            GameSettings gameSettings = ReadJsonFile(filePath);

            foreach (Scenario scenario in gameSettings.scenarios)
            {
                // Debug.Log($"Scenario: {scenario.name}");
                // Debug.Log($"Max Turns: {scenario.settings.maxTurns}, Instructions: {scenario.settings.humanInstructions}");
                GameManager.Instance.maxTurns = scenario.settings.maxTurns;
                // Debug.Log($"Max Turns: {GameManager.Instance.maxTurns}");

                foreach (WinCondition winCondition in scenario.settings.winConditions)
                {
                    // Debug.Log($"Win Condition: {winCondition.condition}, Number: {winCondition.number}");
                    switch (winCondition.condition)
                    {
                        case "Carrot":
                            UIManager.Instance.carrotNeeded = winCondition.number;
                            // Do something with carrotsNeeded
                            break;
                        case "Cabbage":
                            UIManager.Instance.cabbageNeeded = winCondition.number;
                            // Do something with carrotsNeeded
                            break;
                        case "Onion":
                            UIManager.Instance.onionNeeded = winCondition.number;
                            // Do something with carrotsNeeded
                            break;
                        default:
                            Debug.LogError($"No match found for condition: {winCondition.condition}");
                            break;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }


}
