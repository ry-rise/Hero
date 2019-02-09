using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        if (gameManager.GameState == GameManager.GameStatus.Play || gameManager.GameState == GameManager.GameStatus.Wait)
        {
            gameManager.RequestGameState = GameManager.GameStatus.Pause;
        }
    }
}