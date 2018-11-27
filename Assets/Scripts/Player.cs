using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Player : MonoBehaviour
{
    public Class.Type classType = Class.Type.Basic;

    // Float
    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 500f;
    public float dashSpeed = 400f;
    private float respawnTimer;
    public float dashTimer = 0;
    private float dashCd = 0.5f;
    private float dashDelayTimer = 0;
    private float dashDelayCd = 2.0f;

    // Bool 
    public bool grounded;
    public bool dashing;
    public bool wallSliding;
    public bool wallCheck;
    public bool canDoubleJump = false;
    public bool doubleJump = false;
    public bool isDead = false;
    public bool facingRight = true;
    public bool dead = false;
    public bool crowned = false;

    // Int
    public int curHealth;
    public int maxHealth = 100;
    public int damage = 10;
    public int playerNumber = 1;
    public int playerInList = 0;
    public int deathNumber = 0;

    // Reference
    private GameObject CrownObject;
    public GameObject UICrown;
    public GameObject bloodSplatter;
    private Rigidbody2D rb2d;
    private Animator anim;
    public Transform wallCheckPoint;
    public LayerMask wallLayerMask;

    // Override controllers
    public AnimatorOverrideController BasicController;
    public AnimatorOverrideController KnightController;

    // Use this for initialization
    void Awake()
    {
        CrownObject = GameObject.Find("Crown");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        curHealth = maxHealth;
    }

    public void init(int pnumber)
    {
        playerNumber = pnumber + 1;
        playerInList = pnumber;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetBool("DoubleJump", doubleJump);
        anim.SetBool("WallSliding", wallSliding);
        anim.SetBool("Dashing", dashing);
        anim.SetBool("Dead", dead);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        if (Input.GetAxis("Horizontal_P" + playerNumber.ToString()) < -0.1f || Input.GetAxis("LeftJoystick_P" + playerNumber.ToString()) < 0f)
        {
            if (!dead)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                facingRight = false;
            }
        }

        if (Input.GetAxis("Horizontal_P" + playerNumber.ToString()) > 0.1f || Input.GetAxis("LeftJoystick_P" + playerNumber.ToString()) > 0f)
        {
            if (!dead)
            {
                transform.localScale = new Vector3(1, 1, 1);
                facingRight = true;
            }
        }

        if (Input.GetButtonDown("Jump_P" + playerNumber.ToString()))
        {
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    doubleJump = true;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * jumpPower);
                }
            }
        }

        if (Input.GetButtonDown("Dash_P" + playerNumber.ToString()) && !dashing && dashDelayTimer <= 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x * 3f, rb2d.velocity.y);
            dashing = true;
            dashTimer = dashCd;
        }

        if (dashing)
        {
            if (dashTimer > 0)
            {
                if (facingRight)
                    rb2d.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Force);
                else
                    rb2d.AddForce(new Vector2(-dashSpeed, 0), ForceMode2D.Force);
                dashTimer -= Time.deltaTime;
            }
            else
            {
                dashing = false;
                dashDelayTimer = dashDelayCd;
            }
        }

        if (dashDelayTimer > 0)
            dashDelayTimer -= Time.deltaTime;

        if (Input.GetButtonUp("Jump_P" + playerNumber.ToString()))
        {
            if (doubleJump == true)
            {
                doubleJump = false;
            }
        }


        if (curHealth > maxHealth)
            curHealth = maxHealth;
        if (curHealth <= 0)
            StartCoroutine(Die());


        if (!grounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);

            if (facingRight && Input.GetAxis("Horizontal_P" + playerNumber.ToString()) > 0.1f || !facingRight && Input.GetAxis("Horizontal_P" + playerNumber.ToString()) < 0.1f)
            {
                if (wallCheck)
                {
                    HandleWallSliding();
                }
            }
        }

        if (wallCheck == false || grounded)
        {
            wallSliding = false;
        }
    }

    void HandleWallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.9f);

        wallSliding = true;

        canDoubleJump = false;

        if (Input.GetButtonDown("Jump_P" + playerNumber.ToString()))
        {
            if (facingRight)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                facingRight = false;
                rb2d.AddForce(new Vector2(-1, 1) * jumpPower);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                facingRight = true;
                rb2d.AddForce(new Vector2(1, 1) * jumpPower);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;
        float h;

        if (Input.GetAxis("Horizontal_P" + playerNumber.ToString()) != 0)
        {
            h = Input.GetAxis("Horizontal_P" + playerNumber.ToString());
        }
        else
        {
            h = Input.GetAxis("LeftJoystick_P" + playerNumber.ToString()) * 2;
        }

        if (grounded && !dead)
        {
            rb2d.velocity = easeVelocity;
        }

        // Moving the player
        if (grounded && !dead)
        {
            rb2d.AddForce((Vector2.right * speed) * h);
        }
        else if (!grounded && !dead)
        {
            rb2d.AddForce((Vector2.right * speed / 2) * h);
        }

        // Limiting the speed of Player
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

    public void changeClass(Class.Type newclasstype)
    {
        if (newclasstype == Class.Type.Basic && classType != Class.Type.Basic)
        {
            anim.runtimeAnimatorController = BasicController;
        }
        else if (newclasstype == Class.Type.Knight && classType != Class.Type.Knight)
        {
            anim.runtimeAnimatorController = KnightController;
        }
        classType = newclasstype;
    }

    public void Damage(int dam)
    {
        Debug.Log("Current health = " + curHealth);
        curHealth -= dam;
        Debug.Log("After hit = " + curHealth);
        GameManager.instance.GameUIManager.PlayerUIManager[playerInList].SetHP(curHealth);
    }

    private void crownDrop()
    {
        Debug.Log("drop crown");
        CrownObject.transform.position = UICrown.transform.position;
        CrownObject.SetActive(true);
        CrownObject.transform.rotation = Quaternion.identity;
        CrownObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, 400f, 0f));
        UICrown.SetActive(false);
        GameManager.instance.GameUIManager.PlayerUIManager[playerInList].DropCrown();
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.CompareTag("Crown"))
        {
            UICrown.SetActive(true);
            crowned = true;
            GameManager.instance.GameUIManager.PlayerUIManager[playerInList].PickCrown();
            hit.gameObject.SetActive(false);
        }
    }

    IEnumerator Die()
    {
        curHealth = maxHealth;
        if (crowned)
        {
            crownDrop();
            crowned = false;
        }
        deathNumber++;
        dead = true;
        //Camera shake && Bloodsplatter
        Instantiate(bloodSplatter, transform.position, Quaternion.identity);
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        yield return new WaitForSeconds(2f);
        Debug.Log("Set activ false");
        gameObject.SetActive(false);
        dead = false;
    }

    public void Respawn()
    {
        dead = false;
    }
}

