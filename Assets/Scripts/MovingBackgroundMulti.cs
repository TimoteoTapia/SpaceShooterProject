using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovingBackgroundMulti : MonoBehaviour
{
    [SerializeField] [Range(0, 5)] float baseSpeed;
    [SerializeField] BackgroundLayer[] layers;

    void Update()
    {
        foreach (BackgroundLayer layer in layers)
        {
            float deltaY = layer.scrollingSpeed * baseSpeed * Time.deltaTime;
            float newPositionY = layer.transform.position.y - deltaY;
            if (newPositionY < layer.boundary)
                newPositionY += layer.disntance;
            layer.transform.position = new Vector2(layer.transform.position.x, newPositionY);
        }
    }
    [Serializable]
    private class BackgroundLayer
    {
        public Transform transform;
        public float scrollingSpeed, boundary, disntance;


    }
}
