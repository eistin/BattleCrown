using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerUIPrefb;

    [SerializeField]
    private Transform PlayerUITransform;

    public List<PlayerUIManager> PlayerUIManager;

    [SerializeField]
    private GameObject PauseGameMenuObject;
    [SerializeField]
    private GameObject WinningGameMenuObject;

    [SerializeField]
    private TextMeshProUGUI WinnerText;

    public void Init()
    {
        foreach (Transform child in PlayerUITransform)
            Destroy(child.gameObject);
        for (var i = 0; i < GameManager.instance.PlayerCount; i++)
        {
            var PlayerUI = Instantiate(PlayerUIPrefb, PlayerUITransform);
            PlayerUI.GetComponent<PlayerUIManager>().Init(i);
            PlayerUIManager.Add(PlayerUI.GetComponent<PlayerUIManager>());
        }
	}

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseGameMenuObject.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        PauseGameMenuObject.SetActive(false);
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Arena");
    }

    public void WinGame(int idPlayer)
    {
        Time.timeScale = 0;
        WinningGameMenuObject.SetActive(true);
        WinnerText.text = "Player " + (idPlayer + 1) + " won !";
    }
}
