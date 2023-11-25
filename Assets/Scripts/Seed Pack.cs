using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeedPack : MonoBehaviour
{
    public bool isSelected = false;
    public int seedType; // 1 = Carrot 2 = Cabbage 3 = Onion
    private Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnMouseDown()
    {
        Debug.Log("Seed Selected");
        isSelected = true;
        PlantManager.Instance.plantSelected = seedType;
        
    }

    private void OnMouseUp()
    {
        Debug.Log("Seed Deselected");
        isSelected = false;
        transform.position = originPos;
        PlantManager.Instance.plantSelected = 0;   
    }

    private void Movement()
    {
        
        if(isSelected)
        {
            Debug.Log("Seed Moving");
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            transform.position = target;
        }
    }
}
