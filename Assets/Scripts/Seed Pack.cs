using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeedPack : MonoBehaviour
{
    public bool isSelected = false;
    public string seedType;
    private Vector3 originPos;
    private Navigate navigateScript;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        navigateScript = FindObjectOfType<Navigate>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnMouseDown()
    {
        //Debug.Log("Seed Selected");
        isSelected = true;

        //PlantManager.Instance.plantSelected = seedType;

        navigateScript.DisablePathfinding();

    }

    private void OnMouseUp()
    {
        //Debug.Log("Seed Deselected");
        isSelected = false;

        //PlantManager.Instance.plantSelected = 0;

        navigateScript.Invoke("EnablePathfinding", 0.1f);

        transform.position = originPos;
    }

    private void Movement()
    {

        if (isSelected)
        {
            //Debug.Log("Seed Moving");
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            transform.position = target;
        }
    }
}
