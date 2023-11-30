using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public string plantType;
    public GameObject panel;
    public bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        panel = UIManager.Instance.GetPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Plant Selected");
        if(PlantManager.Instance.packSelected)
        {
            return;
        }
        panel.SetActive(true);
        isSelected = true;
        
        
    }

    private void OnMouseUp()
    {
        Debug.Log("Plant Deselected");
        isSelected = false;
        panel.SetActive(false);
    }
}
