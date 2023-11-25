using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growable : MonoBehaviour
{
    int currentStage = 0;
    public SpriteRenderer spriteRenderer; // ��SpriteRenderer������
    public Sprite[] sprites; // Ҫ�л���Sprites����
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
