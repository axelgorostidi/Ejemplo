using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTeraBot : MonoBehaviour
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
    public GameObject Bullet;
    public GameObject lifeOrb;
    public GameObject player;

    private GameObject shootTeraBotSound;
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
        shootTeraBotSound = GameObject.FindGameObjectWithTag("teraBotShootSound");
        hitSound = GameObject.FindGameObjectWithTag("hitSound");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        randX = returnRandomValue();
        randY = returnRandomValue();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        //movimiento random
        if (currentTimerRandMovement > 0) { currentTimerRandMovement -= Time.deltaTime; }
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
            AudioSource hitAudioSource = hitSound.GetComponent<AudioSource>();
            hitAudioSource.Play();
            spriteRenderer.color = damageColor;
            currentColorTime = colorTime;
            life -= 1f;
            if (life <= 0f)
            {
                controlLifeOrb();
                GameManager.game.contEnemies -= 1;
                Destroy(gameObject, 0f);
            }
        }

        if (collision.gameObject.tag == "Scenario")
        {
            randX = returnRandomValue();
            randY = returnRandomValue();
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
        AudioSource shootAudioSource = shootTeraBotSound.GetComponent<AudioSource>();
        shootAudioSource.Play();
        Transform rigidBody_player = player.GetComponent<Transform>();
        float player_posX = rigidBody_player.position.x;
        float player_posY = rigidBody_player.position.y;
        Vector3 playerPos = new Vector3(player_posX, player_posY, 0f);
        Vector3 TeraBotPos = new Vector3(transform.position.x, transform.position.y, 0f);
        Vector3 direction_to_player = playerPos - TeraBotPos;
        float dir_to_player_mod = Convert.ToSingle(Math.Sqrt(direction_to_player[0] * direction_to_player[0] + direction_to_player[1] * direction_to_player[1]));
        Vector3 dir_to_player_versor = new Vector3(direction_to_player[0] / dir_to_player_mod, direction_to_player[1] / dir_to_player_mod, 0f);

        BulletFreeMovement scriptBullet1 = Bullet.GetComponent<BulletFreeMovement>();
        scriptBullet1.dir = dir_to_player_versor + new Vector3(returnRandomSmallValue(), returnRandomSmallValue(), 0f);
        Instantiate(Bullet, transform.position, Quaternion.identity);

        BulletFreeMovement scriptBullet2 = Bullet.GetComponent<BulletFreeMovement>();
        scriptBullet2.dir = dir_to_player_versor + new Vector3(returnRandomSmallValue(), returnRandomSmallValue(), 0f);
        Instantiate(Bullet, transform.position, Quaternion.identity);

        BulletFreeMovement scriptBullet3 = Bullet.GetComponent<BulletFreeMovement>();
        scriptBullet3.dir = dir_to_player_versor + new Vector3(returnRandomSmallValue(), returnRandomSmallValue(), 0f);
        Instantiate(Bullet, transform.position, Quaternion.identity);

        BulletFreeMovement scriptBullet4 = Bullet.GetComponent<BulletFreeMovement>();
        scriptBullet4.dir = dir_to_player_versor + new Vector3(returnRandomSmallValue(), returnRandomSmallValue(), 0f);
        Instantiate(Bullet, transform.position, Quaternion.identity);

        BulletFreeMovement scriptBullet5 = Bullet.GetComponent<BulletFreeMovement>();
        scriptBullet5.dir = dir_to_player_versor + new Vector3(returnRandomSmallValue(), returnRandomSmallValue(), 0f);
        Instantiate(Bullet, transform.position, Quaternion.identity);

        BulletFreeMovement scriptBullet6 = Bullet.GetComponent<BulletFreeMovement>();
        scriptBullet6.dir = dir_to_player_versor + new Vector3(returnRandomSmallValue(), returnRandomSmallValue(), 0f);
        Instantiate(Bullet, transform.position, Quaternion.identity);

        BulletFreeMovement scriptBullet7 = Bullet.GetComponent<BulletFreeMovement>();
        scriptBullet7.dir = dir_to_player_versor + new Vector3(returnRandomSmallValue(), returnRandomSmallValue(), 0f);
        Instantiate(Bullet, transform.position, Quaternion.identity);

        BulletFreeMovement scriptBullet8 = Bullet.GetComponent<BulletFreeMovement>();
        scriptBullet8.dir = dir_to_player_versor + new Vector3(returnRandomSmallValue(), returnRandomSmallValue(), 0f);
        Instantiate(Bullet, transform.position, Quaternion.identity);

    }

    void controlLifeOrb()
    {
        float rand = UnityEngine.Random.Range(-1f, 1f);
        if (rand > 0f)
        {
            lifeOrbController lifeOrbController = lifeOrb.GetComponent<lifeOrbController>();
            Instantiate(lifeOrbController, transform.position, Quaternion.identity);
        }
    }

    float returnRandomSmallValue()
    {
        float rand = UnityEngine.Random.Range(-0.2f, 0.2f);
        return rand;
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