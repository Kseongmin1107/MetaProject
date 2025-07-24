using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyPlaneGameManager : MonoBehaviour
{
    static FlappyPlaneGameManager gameManager;

    public static FlappyPlaneGameManager Instance { get { return gameManager; } }

    private int currentScore = 0;

    FlappyPlaneUIManager uiManager;
    public FlappyPlaneUIManager UIManager { get { return uiManager; } }

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<FlappyPlaneUIManager>();
    }

    private void Start()
    {
        uiManager.UpdateScore(0);
    }

    public void GameOver(bool success) 
    {
        Debug.Log("Game Over");

        if (GameManager.Instance != null) 
        {
            GameManager.Instance.SetMiniGameScore(currentScore);

            GameManager.Instance.SetMiniGameResult(success);

            GameManager.Instance.EndMiniGame();
        }
        else 
        {
            Debug.LogWarning("GameManager 인스턴스를 찾을 수 없습니다. 미니게임을 다시 시작합니다.");        
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    public void AddScore(int score)
    {
        currentScore += score;

        Debug.Log("Score: " + currentScore);
        uiManager.UpdateScore(currentScore);

    }
}