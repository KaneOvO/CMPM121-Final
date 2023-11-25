using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DispalyDay : MonoBehaviour
{
    private  TMP_Text dayText;

    private void OnEnable(){
        dayText = GetComponent<TMP_Text>();
    }
    void Update()
    {
        dayText.text = "Day: " + GameManager.Instance.currentTurn;
    }
}
