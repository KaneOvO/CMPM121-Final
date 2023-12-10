using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growable : MonoBehaviour
{
    public int currentStage = GlobalValue.INITIAL_STAGE;
    private SpriteRenderer spriteRenderer; 
     
    public Sprite[] sprites; 
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[currentStage];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setStage(int stage) {
        currentStage = stage;
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = sprites[currentStage];
    }

    public int getStage() {
        return currentStage;
    }
}
