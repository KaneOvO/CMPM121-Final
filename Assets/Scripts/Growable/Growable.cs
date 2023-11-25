using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growable : MonoBehaviour
{
    int currentStage = 0;
    public SpriteRenderer spriteRenderer; // 对SpriteRenderer的引用
    public Sprite[] sprites; // 要切换的Sprites数组
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = sprites[currentStage];
    }

    public void setStage(int stage) {
        currentStage = stage;
    }

    public int getStage() {
        return currentStage;
    }
}
