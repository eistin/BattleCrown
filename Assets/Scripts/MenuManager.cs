using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Stack<string> MenuHistory;
    private static bool created = false;
    public bool InGame = false;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        created = true;
        MenuHistory = new Stack<string>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Arena")
        {
            if (MenuHistory.Count != 0)
                SceneManager.LoadScene(MenuHistory.Pop());
            else
                ExitGame();
        }
    }
    public void ChangeScene(string SceneToLoad)
    {
        MenuHistory.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
