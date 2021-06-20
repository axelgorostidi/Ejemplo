using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class BulletFreeMovement : MonoBehaviour
{   
    public Vector3 dir;
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
        transform.position += new Vector3(dir[0] * (speedBullet * Time.deltaTime), dir[1]*(speedBullet * Time.deltaTime), 0f);
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0f);
    }
}
