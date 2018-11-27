using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameUIManager GameUIManager;

    [SerializeField]
    private GameObject[] PlayerPrefab;
    [SerializeField]
    private Transform[] PlayerSpawnPoint;

    public List<GameObject> PlayersInstantiate;

    public int PlayerCount { get; private set; }

    void Awake()
    {
        Time.timeScale = 1;
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        PlayerCount = PlayerData.PlayerCount;
        Debug.Log("Player count : " + PlayerCount);
        GameUIManager.Init();
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        for (var i = 0; i < PlayerCount; i++)
        {
            var Player = Instantiate(PlayerPrefab[i]);
            Player.transform.position = PlayerSpawnPoint[i].position;
            Player.GetComponent<Player>().init(i);
            PlayersInstantiate.Add(Player);
        }
    }

    public void RespawnPlayer(int idPlayer)
    {
        PlayersInstantiate[idPlayer].GetComponent<Player>().Respawn();
        PlayersInstantiate[idPlayer].transform.position = PlayerSpawnPoint[idPlayer].position;
        PlayersInstantiate[idPlayer].SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePauseGame();
        }
    }

    private void HandlePauseGame()
    {
        if (Time.timeScale == 0)
            GameUIManager.UnpauseGame();
        else
            GameUIManager.PauseGame();
    }
}
