using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject[] PlayerSelectedObject;

    [SerializeField]
    private GameObject[] PressAKeyObject;

    [SerializeField]
    private GameObject StartTheGameObject;

    private int PlayerCount = 0;

    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.A) && PlayerCount < 4)
        {
            AddPlayer();
        }
        if (Input.GetKeyUp(KeyCode.Z) && PlayerCount != 0)
        {
            RemovePlayer();
        }
        if (Input.GetKeyUp(KeyCode.Return) && PlayerCount >= 2)
        {
            PlayerData.PlayerCount = PlayerCount;
            Application.LoadLevel("Arena");
        }
    }

    private void AddPlayer()
    {
        PressAKeyObject[PlayerCount].SetActive(false);
        PlayerSelectedObject[PlayerCount].SetActive(true);
        PlayerCount++;
        if (PlayerCount == 2)
        {
            StartTheGameObject.SetActive(true);
        }
    }

    private void RemovePlayer()
    {
        PlayerCount--;
        PressAKeyObject[PlayerCount].SetActive(true);
        PlayerSelectedObject[PlayerCount].SetActive(false);
        if (PlayerCount < 2)
        {
            StartTheGameObject.SetActive(false);
        }
    }

}
