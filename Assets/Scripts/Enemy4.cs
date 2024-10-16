using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float moveSpeedHorizontalMax;
    [SerializeField] float moveSpeedVertical1;
    [SerializeField] float moveSpeedVertical2;
    [SerializeField] float chasePosition;

    float moveSpeedHorizontal, moveSpeedVertical;

    void Start()
    {
        moveSpeedVertical = moveSpeedVertical1;
    }


    void Update()
    {
        SetSpeed();
        Move();
    }

    private void Move()
    {
        float deltaX = moveSpeedHorizontal * Time.deltaTime;
        float deltaY = moveSpeedVertical * Time.deltaTime;
        var newPosX = transform.position.x + deltaX;
        var newPosY = transform.position.y - deltaY;
        transform.position = new Vector2(newPosX, newPosY);
        //if (transform.position.y < -11 || Mathf.Abs(transform.position.x) > 8)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void SetSpeed()
    {
        if (transform.position.y < chasePosition)
        {
            moveSpeedVertical = moveSpeedVertical2;
            if (FindObjectOfType<PlayerShip>())
            {
                PlayerShip player = FindObjectOfType<PlayerShip>();
                float playerDistance = player.transform.position.x - transform.position.x;
                moveSpeedHorizontal = /*playerDistance;*/Mathf.Clamp(playerDistance * 1.5f, moveSpeedHorizontalMax * -1, moveSpeedHorizontalMax);
            }
            else
            {
                moveSpeedHorizontal = 0;
            }
        }
    }
}
