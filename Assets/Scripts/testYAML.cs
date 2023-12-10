// using System.Collections.Generic;
// using System.IO;
// using Xunit.Abstractions;
// using YamlDotNet.Core;
// using YamlDotNet.Core.Events;
// using YamlDotNet.Samples.Helpers;
// using YamlDotNet.Serialization;

// namespace YamlDotNet.Samples

// public class TestToml : MonoBehaviour
// {
//     void Start()
//     {
//         string tomlText = File.ReadAllText("Assets/Scripts/testToml.toml");

//         dynamic tomlData = Toml.Parse(tomlText);

//         Debug.Log($"win_conditions: {tomlData.normal.win_conditions}");
//         Debug.Log($"human_instruction: {tomlData.normal.human_instruction}");
        
//     }
// }


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


public static class YamlUtility
{
    // 方法：读取指定路径的 YAML 文件，并返回 Person 对象
    public static GameSettings ReadYamlFile(string filePath)
    {
        try
        {
            string yamlContent = File.ReadAllText(filePath);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var gameSettings = deserializer.Deserialize<GameSettings>(yamlContent);
            return gameSettings;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading YAML file: {ex.Message}", ex);
        }
    }
}

public class GameSettings
{
    public int maxTurns { get; set; }
    public string humanInstructions { get; set; }
    public List<List<object>> winConditions { get; set; } 
}

public class winConditions
{
    public string condition { get; set; }
    public int number { get; set; }
}


