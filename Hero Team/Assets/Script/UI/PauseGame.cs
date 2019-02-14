using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private GameManager manager;
    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(manager.GameState);
    }

    public void GoToTitle()
    {
        GameManager.ScoreReset();
        GameManager.PlayerLife = 3;
        GameManager.StageNumber = 1;
        SceneManager.LoadScene("Title");
    }

    public void Restart()
    {
        if (manager.GameState == GameManager.GameStatus.Pause)
        {
            manager.RequestGameState = GameManager.GameStatus.Play;
        }
    }
}