using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallNextTurnOnChildren()
    {
        foreach (Transform child in transform)
        {
            Land land = child.GetComponent<Land>();
            if (land != null)
            {
                land.NextTurn();
            }
        }
    }
}
