using UnityEngine;

public class YamlExample : MonoBehaviour
{
    void Start()
    {
        try
        {
            // 假设 YAML 文件位于 Assets 文件夹
            string filePath = "Assets/example.yaml";

            // 调用 YamlUtility 来读取 YAML 文件
            var gameSettings = YamlUtility.ReadYamlFile(filePath);

            // 输出结果
            Debug.Log($"maxTurns: {gameSettings.maxTurns}, humanInstructions: {gameSettings.humanInstructions}, WinConditions: {gameSettings.winConditions}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}