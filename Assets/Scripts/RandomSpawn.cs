using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public Transform[] cups;
    public GameObject[] items;

    private float nextActionTime = 10.0f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called randomly
    void RandomSpawnItem()
    {
        int chosenTransform;
        int chosenItem;

        chosenTransform = Random.Range(0, cups.Length);
        chosenItem = Random.Range(0, items.Length);
        GameObject ball = (GameObject)Instantiate(items[chosenItem], cups[chosenTransform].position, Quaternion.identity);
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > nextActionTime)
        {
            RandomSpawnItem();
            nextActionTime += (float)Random.Range(15.0f, 30.0f);
        }
    }
}
