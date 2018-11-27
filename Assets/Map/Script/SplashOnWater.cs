using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashOnWater : MonoBehaviour {

    //public Animator anim;
    public GameObject splash;

    private GameObject splashClone;
    private float delay = 0.40f;

    // Use this for initialization
    /*void Start () {
		
	}*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
     //   if (collision.gameObject.CompareTag("Water"))
       // {
            splashClone = Instantiate(splash, collision.transform.position, Quaternion.identity);


            Destroy(splashClone, delay);

        if (collision.gameObject.CompareTag("Box"))
            Destroy(collision.gameObject);

       // }
        //collision.
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
      //  Destroy(splash);
    }
    // Update is called once per frame
    /*void Update () {
		
	}*/
}
