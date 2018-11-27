using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EZCameraShake;

public class KingIA : MonoBehaviour
{
    public List<GameObject> goal;
    public BoxCollider2D Feet;
    public GameObject timeManager;
    public GameObject LifeBar;
    public GameObject Crown;
    public GameObject UICrown;
    public GameObject bloodSplatter;

    private Animator anim;
    private GameObject focus;
    private System.Random random;
    private Vector3 SpriteScale;
    private Vector3 UIScale;

    private bool facingRight = true;
    private float timer;
    private bool grounded = false;
    private bool death;

    // Use this for initialization
    void Start()
    {
        Crown = GameObject.Find("Crown");
        Crown.SetActive(false);
        goal = GameManager.instance.PlayersInstantiate;
        death = false;
        random = new System.Random();
        anim = GetComponent<Animator>();
        if (goal.Capacity != 0)
            focus = goal[random.Next(0, goal.Count)];
        timer = 5;
    }

    void FixedUpdate()
    {
        if (LifeBar.transform.localScale.x <= 0)
        {
            anim.SetTrigger("death");
            timer -= Time.deltaTime;
            if (timer <= 0)
                gameObject.SetActive(false);
        }
        if (focus == null || transform.position.y < focus.transform.position.y)
            GetComponent<PlatformEffector2D>().rotationalOffset = 180;
    }

    // Update is called once per frame    
    void Update()
    {
        //                              DEBUG and TEST       
        if (Input.GetMouseButtonDown(1) && LifeBar.transform.localScale.x > 0)
            ChangeLife(10);
        else if (Input.GetMouseButtonDown(0) && LifeBar.transform.localScale.x < 1)
            ChangeLife(-10);


        if (death == true || goal.Capacity == 0)
            return;

        ChangeFocus();   
        KingAction();
    }

    void ChangeFocus()
    {
        if (timer < 0 || focus == null || !focus.activeInHierarchy)
        {
            focus = goal[random.Next(0, goal.Count)];
            timer = 3;
        }
        else
            timer -= Time.deltaTime;
    }

    void KingAction()
    {
        if (transform.position.x < focus.transform.position.x && !facingRight)
            Flip();
        else if (transform.position.x > focus.transform.position.x && facingRight)
            Flip();

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("skill_1"))
        {
            if (grounded == true && transform.position.y < focus.transform.position.y - 0.6f)
            {
                GetComponent<PlatformEffector2D>().rotationalOffset = 180;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, (focus.transform.position.y - (transform.position.y - 1f)) * 3.5f);
                grounded = false;
            }
            else if (grounded == true && transform.position.y > focus.transform.position.y + 0.6f)
                GetComponent<PlatformEffector2D>().rotationalOffset = 0;

            if (transform.position.x < focus.transform.position.x - 1)
            {
                anim.SetTrigger("run");
                GetComponent<Rigidbody2D>().velocity = new Vector2(3f, GetComponent<Rigidbody2D>().velocity.y);
                if (!facingRight)
                    Flip();
            }
            else if (transform.position.x > focus.transform.position.x + 1)
            {
                anim.SetTrigger("run");
                GetComponent<Rigidbody2D>().velocity = new Vector2(-3f, GetComponent<Rigidbody2D>().velocity.y);
                if (facingRight)
                    Flip();
            }
        }
        else
        {
            anim.SetTrigger("idle_1");
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    //  change direction of character
    void Flip()
    {
        facingRight = !facingRight;
        SpriteScale = transform.localScale;
        SpriteScale.x *= -1;
        transform.localScale = SpriteScale;

        UIScale = GetComponentInChildren<Canvas>().transform.localScale;
        UIScale.x *= -1;
        GetComponentInChildren<Canvas>().transform.localScale = UIScale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (LifeBar.transform.localScale.x <= 0)
            return;
        if (collision.gameObject.tag == "Player" && !anim.GetCurrentAnimatorStateInfo(0).IsName("skill_1"))
            anim.SetTrigger("skill_1");
        if (collision.IsTouching(Feet))
            grounded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && anim.GetCurrentAnimatorStateInfo(0).IsName("skill_1"))
            collision.SendMessageUpwards("Damage", 10);
    }


    void ChangeLife(float Value)
    {
        if (LifeBar.transform.localScale.x > 0)
            LifeBar.GetComponent<KingHealth>().UpdateHealth(Value);
        else
            return;

        // Check King life
        if (LifeBar.transform.localScale.x <= 0)
        {
            int Propultion = 0;

            death = true;

            UICrown.SetActive(false);
            Crown.SetActive(true);

            Crown.transform.position = new Vector3(transform.position.x, UICrown.transform.position.y + 1);
            Crown.transform.rotation = Quaternion.identity;

            // Bloodsplatter && slow motion && Camera shake
            Instantiate(bloodSplatter, transform.position, Quaternion.identity);


            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);

            Instantiate(timeManager, null);
            timeManager.GetComponent<TimeManager>().DoSlowMotion();
                        
            /*                                  Drop Crown 
            Propultion = random.Next(1, 40);
            Propultion = Propultion % 2 == 0 ? Propultion * -1 : Propultion;

            Crown.GetComponent<Rigidbody2D>().AddForce(new Vector3(Propultion * 10, 400, 0));
            Crown.GetComponent<Rigidbody2D>().AddTorque(Propultion * 7, ForceMode2D.Force);
            */

            Crown.GetComponent<Rigidbody2D>().AddForce(new Vector3(50, 400, 0));
            Crown.GetComponent<Rigidbody2D>().AddTorque(100, ForceMode2D.Force);
            timer = 5;
        }
    }
}
