using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    [SerializeField] float minAlpha;

    SpriteRenderer spriteRenderer;

    float alpha;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alpha = 1;
    }


    void Update()
    {
        alpha -= 5f * Time.deltaTime;
        if (alpha < minAlpha)
            alpha = 1;
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
    }
}
