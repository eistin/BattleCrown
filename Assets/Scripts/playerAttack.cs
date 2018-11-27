using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    private bool attacking = false;

    private float attackTimer = 0;
    private float attackCd = 0.3f;

    Player player;
    public Collider2D attackTrigger;
    private Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

    private void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack_P" + player.playerNumber.ToString()) && !attacking)
        {
            attacking = true;
            player.dashTimer = 0;
            player.dashing = false;
            attackTimer = attackCd;

            attackTrigger.enabled = true;
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
            }
        }

        anim.SetBool("Attacking", attacking);
        anim.SetBool("Dashing", player.dashing);
    }
}