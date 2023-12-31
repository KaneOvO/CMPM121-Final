using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeedPack : MonoBehaviour
{
    public bool isSelected = false;
    public PlantType seedType; 
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
        
        isSelected = true;

        PlantManager.Instance.packSelected = isSelected;

        navigateScript.DisablePathfinding();

    }

    private void OnMouseUp()
    {
       
        isSelected = false;

        PlantManager.Instance.packSelected = isSelected;

        navigateScript.Invoke("EnablePathfinding", 0.1f);

        transform.position = originPos;
    }

    private void Movement()
    {

        if (isSelected)
        {
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            transform.position = target;
        }
    }
}

