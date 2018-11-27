using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slowdownFactor = 0.05f;
    public float slowdownTime = 2f;


    // Update is called once per frame
    void Update () {
        //ReUpdate time
        Time.timeScale += (1f / slowdownTime) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        if (Time.timeScale == 1f)
        {
            Destroy(gameObject);
        }
	}

    public void DoSlowMotion() {
        Time.timeScale = slowdownFactor;
        //Time.fixedDeltaTime = Time.timeScale * .02f;

    }
}
