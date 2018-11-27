using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = gameObject.GetComponentInParent<Player>();
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision degat player :" + player);
            collision.SendMessageUpwards("Damage", player.damage);
        }
        else if (collision.gameObject.tag == "King")
        {
            Debug.Log("Collision degat king :" + player);
            collision.SendMessageUpwards("ChangeLife", player.damage);
        }
    }
}
