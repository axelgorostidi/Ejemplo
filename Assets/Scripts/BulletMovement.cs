using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Direction { quiet ,up, upRight, right, downRight, down, downLeft, left, upLeft };

//https://histeriagamedev.wordpress.com/2014/12/20/unity3d-creacion-de-sistema-de-ataque-a-distancia-shurikens-en-juego-2d/

public class BulletMovement : MonoBehaviour
{
    public Direction dirBullet;
    public float speedBullet = 6f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (dirBullet)
        {
            case Direction.up:
                transform.position += new Vector3(0f, (speedBullet * Time.deltaTime), 0f);
                break;
            case Direction.upRight:
                transform.position += new Vector3(speedBullet * Time.deltaTime, speedBullet * Time.deltaTime, 0f);
                break;
            case Direction.right:
                transform.position += new Vector3(speedBullet * Time.deltaTime, 0f, 0f);
                break;
            case Direction.downRight:
                transform.position += new Vector3(speedBullet * Time.deltaTime, -speedBullet * Time.deltaTime, 0f);
                break;
            case Direction.down:
                transform.position += new Vector3(0f, -speedBullet * Time.deltaTime, 0f);
                break;
            case Direction.downLeft:
                transform.position += new Vector3(-speedBullet * Time.deltaTime, -speedBullet * Time.deltaTime, 0f);
                break;
            case Direction.left:
                transform.position += new Vector3(-speedBullet * Time.deltaTime, 0f, 0f);
                break;
            case Direction.upLeft:
                transform.position += new Vector3(-speedBullet * Time.deltaTime, speedBullet * Time.deltaTime, 0f);
                break;

        }

        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0f);
    }
}
