using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMecaSpider : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed = 2f;
    private Vector2 move;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private float randX;
    private float randY;

    public float timerRandMovement = 2f;
    public float currentTimerRandMovement = 2f;

    public float timerRandShoot = 2f;
    public float currentTimerRandShoot = 2f;
    public float typeShoot = 1;
    public GameObject Bullet;
    public GameObject lifeOrb;


    private GameObject shootMecaSpiderSound;
    private GameObject hitSound;

    //Sprite
    private Color originalColor;
    public Color damageColor;
    public float colorTime;
    public float currentColorTime;

    //Mecanics
    public float life;

    // Start is called before the first frame update
    void Start()
    {
        shootMecaSpiderSound = GameObject.FindGameObjectWithTag("mecaSpiderShootSound");
        hitSound = GameObject.FindGameObjectWithTag("hitSound");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        randX = returnRandomValue();
        randY = returnRandomValue();
    }

    // Update is called once per frame
    void Update()
    {

        //movimiento random
        if (currentTimerRandMovement > 0){currentTimerRandMovement -= Time.deltaTime;}
        if (currentTimerRandMovement <= 0)
        {
            randX = returnRandomValue();
            randY = returnRandomValue();

            currentTimerRandMovement = timerRandMovement + UnityEngine.Random.Range(0f, 1f);
        }

        //disparo random
        if (currentTimerRandShoot > 0) { currentTimerRandShoot -= Time.deltaTime; }
        if (currentTimerRandShoot <= 0)
        {
            shoot();
            currentTimerRandShoot = timerRandShoot + UnityEngine.Random.Range(0f, 1f);
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

        if (spriteRenderer.color != originalColor)
        {
            manageColorDamage();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            //hitSound.Play();
            AudioSource hitAudioSource = hitSound.GetComponent<AudioSource>();
            hitAudioSource.Play();
            spriteRenderer.color = damageColor;
            currentColorTime = colorTime;
            life -= 1f;
            if(life <= 0f)
            {
                controlLifeOrb();
                GameManager.game.contEnemies -= 1;
                Destroy(gameObject, 0f);
            }
        }

        if (collision.gameObject.tag == "Scenario")
        {
            randX = -randX;
            randY = -randY;
        }

    }

    void manageColorDamage()
    {
        if (currentColorTime > 0)
        {
            currentColorTime -= Time.deltaTime;
        }
        else
        {
            spriteRenderer.color = originalColor;
        }
    }

    void shoot()
    {
        //shootMecaSpiderSound.Play();
        AudioSource shootAudioSource = shootMecaSpiderSound.GetComponent<AudioSource>();
        shootAudioSource.Play();
        typeShoot = returnRandomValue();
        Debug.Log(typeShoot);


        if (typeShoot == 1)
        {
            BulletMovement scriptBullet1 = Bullet.GetComponent<BulletMovement>();
            scriptBullet1.dirBullet = Direction.up;
            Instantiate(Bullet, transform.position, Quaternion.identity);
            BulletMovement scriptBullet2 = Bullet.GetComponent<BulletMovement>();
            scriptBullet2.dirBullet = Direction.down;
            Instantiate(Bullet, transform.position, Quaternion.identity);
            BulletMovement scriptBullet3 = Bullet.GetComponent<BulletMovement>();
            scriptBullet3.dirBullet = Direction.left;
            Instantiate(Bullet, transform.position, Quaternion.identity);
            BulletMovement scriptBullet4 = Bullet.GetComponent<BulletMovement>();
            scriptBullet4.dirBullet = Direction.right;
            Instantiate(Bullet, transform.position, Quaternion.identity);
        }
        else
        {
            BulletMovement scriptBullet1 = Bullet.GetComponent<BulletMovement>();
            scriptBullet1.dirBullet = Direction.upRight;
            Instantiate(Bullet, transform.position, Quaternion.identity);
            BulletMovement scriptBullet2 = Bullet.GetComponent<BulletMovement>();
            scriptBullet2.dirBullet = Direction.downRight;
            Instantiate(Bullet, transform.position, Quaternion.identity);
            BulletMovement scriptBullet3 = Bullet.GetComponent<BulletMovement>();
            scriptBullet3.dirBullet = Direction.downLeft;
            Instantiate(Bullet, transform.position, Quaternion.identity);
            BulletMovement scriptBullet4 = Bullet.GetComponent<BulletMovement>();
            scriptBullet4.dirBullet = Direction.upLeft;
            Instantiate(Bullet, transform.position, Quaternion.identity);
        }
    }

    void controlLifeOrb()
    {
        float rand = UnityEngine.Random.Range(-1f, 1f);
        if(rand > 0.5f)
        {
            lifeOrbController lifeOrbController = lifeOrb.GetComponent<lifeOrbController>();
            Instantiate(lifeOrbController, transform.position, Quaternion.identity);
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