using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    public string plantType;
    public GameObject panel;
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
        panel.SetActive(true);
    }
}
