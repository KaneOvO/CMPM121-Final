using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Awake()
    {
        CreateMainCamera();
        CreateLight2D();
    }
    void CreateMainCamera()
    {
        GameObject mainCamera = new GameObject("Main Camera");
        mainCamera.AddComponent<Camera>();

    }

    void CreateLight2D()
    {
        GameObject light2D = new GameObject("Light2D");
        light2D.AddComponent<UnityEngine.Rendering.Universal.Light2D>();

    }
}
