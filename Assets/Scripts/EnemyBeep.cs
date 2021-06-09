using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBeep : MonoBehaviour { 

    private Rigidbody2D rb;
    public float speed = 2f;
    private Vector2 move;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private float randX;
    private float randY;
    public float timerRandMovement = 2f;
    public float currentTimerRandMovement = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        randX = returnRandomValue();
        randY = returnRandomValue();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTimerRandMovement > 0)
        {
            currentTimerRandMovement -= Time.deltaTime;
        }

        if (currentTimerRandMovement <= 0)
        {
            randX = returnRandomValue();
            randY = returnRandomValue();

            currentTimerRandMovement = timerRandMovement + UnityEngine.Random.Range(0f, 1f);
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position += new Vector3(randX * speed * Time.deltaTime, randY * speed * Time.deltaTime, 0f);

        if (Math.Abs(randX) > 0 || Math.Abs(randY) > 0)
        { 
            if (randX < 0 && spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
            if (randX > 0 && spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject, 0f);
        }

        if (collision.gameObject.tag == "Scenario")
        {
            randX = returnRandomValue();
            randY = returnRandomValue();
        }

    }

    float returnRandomValue()
    {
        float rand = UnityEngine.Random.Range(-1f, 1f);
        if (rand > 0)
        {
            rand = 1;
        }
        else
        {
            rand = -1;
        }
        return rand;
    }
}
