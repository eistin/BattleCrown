using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHealth : MonoBehaviour {

    public float life;
	// Use this for initialization
	void Start () {
        life = 1;
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void UpdateHealth(float update)
    {
        life -= update / 100;
        transform.localScale = new Vector3(life, 1, 3);
    }

}
