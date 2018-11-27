using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
//    private float timer = 0f;
//    private float timer = 0f;

//    timer += Time.deltaTime;
//        if (timer > waitingTime)
//        {
//            timer = 0f;
//            DoThis();
//}

// Use this for initialization
void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Rigidbody2D>().sharedMaterial = null;
            var newPlayerHealth = collision.gameObject.GetComponent<Player>().curHealth;
            newPlayerHealth += 20;
            if (newPlayerHealth > 100)
                newPlayerHealth = 100;

            collision.gameObject.GetComponent<Player>().curHealth = newPlayerHealth;

            var playerInList = collision.gameObject.GetComponent<Player>().playerInList;
            GameManager.instance.GameUIManager.PlayerUIManager[playerInList].SetHP(newPlayerHealth);
            Destroy(gameObject);
        }
    }
}
