using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpark : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.clear; //new Color (0f, 0f, 0f, 0f);
    }
    void Update()
    {
        HideSpark();
    }
    void HideSpark()
    {
        if (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, spriteRenderer.color.a - (Time.deltaTime * 5f));
        }
    }

    public void ShowSpark()
    {
        spriteRenderer.color = Color.white;// new Color(1f, 1f, 1f, 1f)
    }
}
