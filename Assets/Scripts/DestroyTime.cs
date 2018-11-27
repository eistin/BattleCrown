using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{

    public float duration;
    //public float nextTime;

    // Use this for initialization
    //void Start()
    //{
    //    time = 15.0f;/*(float)Random.Range(15.0f, 30.0f);*/
    //    nextTime = time / 100;
    //}

    //void Update()
    //{
    //    if (Time.time >= nextTime)
    //    {
    //        Color tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
    //        tmpColor.a -= 0.15f;
    //        gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
    //        nextTime += nextTime;
    //    }
    //    if (Time.time > time)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public float minimum = 1f;
    public float maximum = 0f;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        duration = (float)Random.Range(15.0f, 30.0f);
    }
    void Update()
    {
        float t = (Time.time - startTime) / duration;
        Color tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
        tmpColor = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.SmoothStep(minimum, maximum, t));
        gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
        if (gameObject.GetComponent<SpriteRenderer>().color.a <= 0.01)
        {
            Destroy(gameObject);
        }
    }
}