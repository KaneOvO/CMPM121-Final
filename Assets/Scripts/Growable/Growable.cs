using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growable : MonoBehaviour
{
    int currentStage = 0;
    private SpriteRenderer spriteRenderer; 
    public Sprite[] sprites; 
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStage(int stage) {
        currentStage = stage;
        spriteRenderer.sprite = sprites[currentStage];
    }

    public int getStage() {
        return currentStage;
    }
}
