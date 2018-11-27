using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{

    private Player player;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Water" || collision.gameObject.tag == "Box" || collision.gameObject.tag == "Player")
            player.grounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Water" || collision.gameObject.tag == "Box" || collision.gameObject.tag == "Player")
            player.grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box" || collision.gameObject.tag == "Player")
            player.grounded = false;
    }
}
