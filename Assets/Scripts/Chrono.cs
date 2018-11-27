using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chrono : MonoBehaviour {

    public float chrono = 0.00f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        chrono += Time.deltaTime;
	}
}
