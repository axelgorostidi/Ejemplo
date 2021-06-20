using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{

    public float speed;
    private Vector2 move;
    private Rigidbody2D rb;
    public GameObject Bullet;
    public float startBulletTimerFreeze;
    public float currentBulletTimerFreeze;
    private bool isShooting;

    private float timeStore;

    public float dashForce;
    public float startDashTimer;
    public float currentDashTimer;
    public float startDashTimerFreeze;
    public float currentDashTimerFreeze;
    private bool isDashing;

    private SpriteRenderer spriteRenderer;
    private Animator anim;
    public bool walk;

    private GameObject camGO;
    private GameObject roomIni;

    //UI
    public float lifeMax = 100f;
    public float lifeCurrent = 100f;
    public Image lifeBar;
    public Image dashBar;
    public Text roomsPassedText;

    //Sprite
    private Color originalColor;
    public Color damageColor;
    public float colorTime;
    public float currentColorTime;

    // Start is called before the first frame update
    void Start()
    {
        camGO = GameObject.FindGameObjectWithTag("MainCamera");
        roomIni = GameObject.FindGameObjectWithTag("roomIni");
        isDashing = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {

        lifeBar.fillAmount = lifeCurrent / lifeMax;
        dashBar.fillAmount = (startDashTimerFreeze - currentDashTimerFreeze)/2;// currentDashTimerFreeze /startDashTimerFreeze;
        roomsPassedText.text = (GameManager.game.contRoomsPassed).ToString();

        anim.SetBool("walk", walk);

        //movimiento WASD------------------------------
        if (Input.GetKey("w"))
        {
            move.y = 1;
        }
        if (Input.GetKey("a"))
        {
            move.x = -1;
        }
        if (Input.GetKey("s"))
        {
            move.y = -1;
        }
        if (Input.GetKey("d"))
        {
            move.x = 1;
        }
        if(!Input.GetKey("d") && !Input.GetKey("a"))
        {
            move.x = 0;
        }
        if (!Input.GetKey("s") && !Input.GetKey("w"))
        {
            move.y = 0;
        }
        float moveX = move.x * speed;
        float moveY = move.y * speed;
        transform.position += new Vector3(moveX * speed * Time.deltaTime, moveY * speed * Time.deltaTime, 0f);
        
        if(Math.Abs(moveX) > 0 || Math.Abs(moveY) > 0){ //para la animacion
            if (moveX < 0 && spriteRenderer.flipX == false){
                spriteRenderer.flipX = true;
            }
            if (moveX > 0 && spriteRenderer.flipX == true){
                spriteRenderer.flipX = false;
            }
            walk = true;
        }
        else{
            walk = false;
        }

        //----------------------------------------------
        dash(moveX, moveY);
        bulletShoot();
        if(spriteRenderer.color != originalColor)
        {
            manageColorDamage();
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "BulletEnemy")
        {
            lifeCurrent -= 10f;
            spriteRenderer.color = damageColor;
            currentColorTime = colorTime;
            checkDead();
        }

        if (collision.gameObject.tag == "Enemy")
        {
            lifeCurrent -= 10f;
            spriteRenderer.color = damageColor;
            currentColorTime = colorTime;
            checkDead();
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

    void checkDead()
    {
        if (lifeCurrent <= 0)
        {
            transform.position = new Vector3(10000f, 10000f, 0f);
            lifeCurrent = 100f;
            GameManager.game.contRoomsPassed = 0;
            Transform rigidBody_cam = camGO.GetComponent<Transform>();
            rigidBody_cam.position = new Vector3(10000f, 10000f, -10f);
            GameManager.game.destroyRooms();
           //Transform rigidBody_room = roomIni.GetComponent<Transform>();
            //rigidBody_room.position = new Vector3(100f, 100f, 0f);
        }
    }

    void dash(float moveX, float moveY)
    {

        if(currentDashTimerFreeze > 0)
        {
            currentDashTimerFreeze -= Time.deltaTime;
        }

        float dirX = moveX != 0 ? (moveX / Math.Abs(moveX)) : 0;
        float dirY = moveY != 0 ? (moveY / Math.Abs(moveY)) : 0;

        if (Input.GetKey("space") && currentDashTimerFreeze <= 0 && isDashing == false)
        {
            isDashing = true;
            currentDashTimer = startDashTimer;
            return;
        }

        if (isDashing)
        {
            rb.AddForce(new Vector3(moveX * dashForce * Time.deltaTime, moveY * dashForce * Time.deltaTime, 0f), ForceMode2D.Impulse);
            currentDashTimer -= Time.deltaTime;
            if (currentDashTimer <= 0)
            {
                isDashing = false;
                currentDashTimerFreeze = startDashTimerFreeze;
            }
        }
        //rb.velocity = Vector2.zero;
    }

    void bulletShoot()
    {
        if (currentBulletTimerFreeze > 0)
        {
            currentBulletTimerFreeze -= Time.deltaTime;
        }

        if ((Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right")) && currentBulletTimerFreeze <= 0 && isShooting == false)
        {
            isShooting = true;
            return;
        }

        if (Bullet != null && (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right")) && isShooting == true)
        {
            BulletMovement scriptBullet = Bullet.GetComponent<BulletMovement>();
            if (Input.GetKey("up") && !Input.GetKey("right") && !Input.GetKey("left") && !Input.GetKey("down"))
            {
                //Ataque hacia arriba
                scriptBullet.dirBullet = Direction.up;
            }
            else if (!Input.GetKey("up") && !Input.GetKey("right") && !Input.GetKey("left") && Input.GetKey("down"))
            {
                //Ataque hacia abajo
                scriptBullet.dirBullet = Direction.down;
            }
            else if (!Input.GetKey("up") && Input.GetKey("right") && !Input.GetKey("left") && !Input.GetKey("down"))
            {
                //Ataque hacia la derecha
                scriptBullet.dirBullet = Direction.right;
            }
            else if (!Input.GetKey("up") && !Input.GetKey("right") && Input.GetKey("left") && !Input.GetKey("down"))
            {
                //Ataque hacia la izquierda
                scriptBullet.dirBullet = Direction.left;
            }
            else if (Input.GetKey("up") && Input.GetKey("right") && !Input.GetKey("left") && !Input.GetKey("down")) //******************************
            {
                //Ataque hacia arriba-derecha
                scriptBullet.dirBullet = Direction.upRight;
            }
            else if (!Input.GetKey("up") && Input.GetKey("right") && !Input.GetKey("left") && Input.GetKey("down"))
            {
                //Ataque hacia la abajo-derecha
                scriptBullet.dirBullet = Direction.downRight;
            }
            else if (!Input.GetKey("up") && !Input.GetKey("right") && Input.GetKey("left") && Input.GetKey("down"))
            {
                //Ataque hacia la abajo-izquierda
                scriptBullet.dirBullet = Direction.downLeft;

            }
            else if (Input.GetKey("up") && !Input.GetKey("right") && Input.GetKey("left") && !Input.GetKey("down"))
            {
                //Ataque hacia la arriba-izquierda
                scriptBullet.dirBullet = Direction.upLeft;
            }

            isShooting = false;
            currentBulletTimerFreeze = startBulletTimerFreeze;
            //Creamos una instancia del prefab en nuestra escena, concretamente en la posición de nuestro personaje
            Instantiate(Bullet, transform.position, Quaternion.identity);
        }
    }
}
