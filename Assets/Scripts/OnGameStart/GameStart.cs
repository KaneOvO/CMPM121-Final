using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    List<GameObject> gameObjectList = new List<GameObject>();
    Transform managers; // Declare the managers variable

    void OnEnable()
    {
        List<string> createList = new List<string>
        {
            "Main Camera",
            "Global Light 2D",
            "Managers",
            "Game Manager",
            "Pool Manager",
            "Map"
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
        gameObjectList.Add(mainCamera);
    }

    void CreateGlobalLight2D()
    {
        GameObject globalLight2D = new GameObject("Global Light 2D");
        globalLight2D.AddComponent<UnityEngine.Rendering.Universal.Light2D>();
        globalLight2D.GetComponent<UnityEngine.Rendering.Universal.Light2D>().lightType = UnityEngine.Rendering.Universal.Light2D.LightType.Global;
        gameObjectList.Add(globalLight2D);
    }

    void CreateManagers()
    {
        managers = new GameObject("Managers").transform; // Initialize the managers variable
        gameObjectList.Add(managers.gameObject);
    }

    void CreateGameManager()
    {
        GameObject gameManager = new GameObject("Game Manager");
        gameManager.AddComponent<GameManager>();
        gameManager.transform.parent = managers; // Set the parent using transform.parent
        gameObjectList.Add(gameManager);
    }

    void CreatePoolManager()
    {
        GameObject poolManager = new GameObject("Pool Manager");
        poolManager.AddComponent<PoolManager>();
        poolManager.transform.parent = managers; // Set the parent using transform.parent
        gameObjectList.Add(poolManager);
    }

    void CreateMap()
    {
        GameObject mapPrefab = Resources.Load<GameObject>("Prefabs/Map");

        if (mapPrefab != null)
        {
            Instantiate(mapPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            gameObjectList.Add(mapPrefab);
        }
        else
        {
            Debug.LogError("Prefab 'Map' not found in Resources folder.");
        }
    }
}
