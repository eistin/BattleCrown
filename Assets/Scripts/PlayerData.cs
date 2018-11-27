using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static int PlayerCount { get; set; }

    [SerializeField]
    private bool isTest = false;

    void Awake()
    {
        if (isTest)
            PlayerCount = 2;
        DontDestroyOnLoad(this);
    }
}
