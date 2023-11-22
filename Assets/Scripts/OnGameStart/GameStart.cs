using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void OnEnable()
    {
        List<string> createList = new List<string>
        {
            "Main Camera",
            "Global Light 2D",
            "Game Manager",
            "Grid"
        };

        foreach (string objName in createList)
        {
            CreateObjectByName(objName);
        }
    }

    void CreateObjectByName(string objName)
    {
        // get function name
        string functionName = "Create" + objName.Replace(" ", "");

        // using reflection to call function
        Type type = typeof(GameStart);
        System.Reflection.MethodInfo method = type.GetMethod(functionName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

        if (method != null)
        {
            method.Invoke(this, null);
        }
        else
        {
            Debug.LogError("Create function not found for object: " + objName + " function name:" + functionName);
        }
    }

    void CreateMainCamera()
    {
        GameObject mainCamera = new GameObject("Main Camera");
        mainCamera.transform.position = new Vector3(0, 0, -10);
        mainCamera.AddComponent<Camera>();
        mainCamera.tag = "MainCamera"; // Set the tag to MainCamera
        mainCamera.AddComponent<AudioListener>(); // Add an AudioListener for audio
    }

    void CreateGlobalLight2D()
    {
        GameObject globalLight2D = new GameObject("Global Light 2D");
        globalLight2D.AddComponent<UnityEngine.Rendering.Universal.Light2D>();
        globalLight2D.GetComponent<UnityEngine.Rendering.Universal.Light2D>().lightType = UnityEngine.Rendering.Universal.Light2D.LightType.Global;
    }

    void CreateGameManager()
    {
        GameObject gameManager = new GameObject("Game Manager");
        gameManager.AddComponent<GameManager>();
    }

    void CreateGrid()
    {
        GameObject gridPrefab = Resources.Load<GameObject>("Prefabs/Grid");

        if (gridPrefab != null)
        {
            // 实例化 Prefab
            Instantiate(gridPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab 'Grid' not found in Resources folder.");
        }
    }
}
