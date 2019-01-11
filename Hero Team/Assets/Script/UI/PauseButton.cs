using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private bool isPaused;
    private GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            if (gameManager.GameState == GameManager.GameStatus.Play || gameManager.GameState == GameManager.GameStatus.Wait)
            {
                gameManager.RequestGameState = GameManager.GameStatus.Pause;
                isPaused = true;
            }
        }
        else
        {
            if (gameManager.GameState == GameManager.GameStatus.Pause)
            {
                gameManager.RequestGameState = GameManager.GameStatus.Play;
                isPaused = false;
            }
        }
    }
}