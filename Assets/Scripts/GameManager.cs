using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ� ����
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [Header("Scene Settings")]
    public string mainMapSceneName = "MetaverseMainScene"; // ���� �� �� �̸�
    public string miniGameSceneName = "FlappyPlaneScene"; // �̴ϰ��� �� �̸�

    [Header("Mini-Game Data")]
    public int currentMiniGameScore = 0; // ���� �̴ϰ��ӿ��� ȹ���� ����
    public int highestMiniGameScore = 0; // ����� �ְ� ����
    public bool didMiniGameSucceed = false; // �̴ϰ��� ���� ����

    private MainMapUIController mainMapUIController;

    private void Awake()
    {
        // �̱��� ���� ����: �̹� �ν��Ͻ��� �ִٸ� �� ������Ʈ �ı�
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighestScore();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == mainMapSceneName)
        {
            mainMapUIController = FindObjectOfType<MainMapUIController>();
            if (mainMapUIController != null)
            {
                Debug.Log("GameManager: MainMapUIController found in Main Map Scene.");
            }
            else
            {
                Debug.LogWarning("GameManager: MainMapUIController not found in Main Map Scene.");
            }
        }
    }

    // �̴ϰ������� ������ �� ȣ�� (��: ���� �ʿ��� �̴ϰ��� Ʈ���ſ� ����� ��)
    public void StartMiniGame()
    {
        Debug.Log("GameManager: Starting Mini-Game.");
        currentMiniGameScore = 0; // �̴ϰ��� ���� �� ���� �ʱ�ȭ
        didMiniGameSucceed = false; // �̴ϰ��� ���� ���� �ʱ�ȭ
        SceneManager.LoadScene(miniGameSceneName); // �̴ϰ��� �� �ε�
    }

    public void EndMiniGame()
    {
        Debug.Log("GameManager: Ending Mini-Game. Returning to Main Map.");
        // �ְ� ���� ���� �� ����
        CheckAndSaveHighestScore(currentMiniGameScore);

        SceneManager.LoadScene(mainMapSceneName); // ���� �� �� �ε�

        StartCoroutine(ShowResultUIWhenSceneReady());
    }

    private IEnumerator ShowResultUIWhenSceneReady()
    {
        yield return null;

        MainMapUIController uiController = FindObjectOfType<MainMapUIController>();
        if (uiController != null)
        {
            uiController.ShowMiniGameResultUI();
        }
        else
        {
            Debug.LogWarning("GameManager: MainMapUIController not found after scene load. Cannot display mini-game result UI.");
        }
    }

    // ���� �̴ϰ��� ������ ����
    public void SetMiniGameScore(int score)
    {
        currentMiniGameScore = score;
        Debug.Log($"GameManager: Mini-Game Score set to {currentMiniGameScore}");
    }

    // �̴ϰ��� ���� ���θ� ����
    public void SetMiniGameResult(bool success)
    {
        didMiniGameSucceed = success;
        Debug.Log($"GameManager: Mini-Game Result: {(success ? "SUCCESS" : "FAILED")}");
    }

    // �ְ� ���� Ȯ�� �� ����
    private void CheckAndSaveHighestScore(int newScore)
    {
        if (newScore > highestMiniGameScore)
        {
            highestMiniGameScore = newScore;
            PlayerPrefs.SetInt("HighestMiniGameScore", highestMiniGameScore);
            PlayerPrefs.Save(); 
            Debug.Log($"GameManager: New Highest Score: {highestMiniGameScore}");
        }
    }


    private void LoadHighestScore()
    {
        highestMiniGameScore = PlayerPrefs.GetInt("HighestMiniGameScore", 0); // "HighestMiniGameScore" Ű�� ����� ���� �ε�, ������ 0
        Debug.Log($"GameManager: Loaded Highest Score: {highestMiniGameScore}");
    }

    public void ResetGameData()
    {
        PlayerPrefs.DeleteAll();
        highestMiniGameScore = 0;
        currentMiniGameScore = 0;
        didMiniGameSucceed = false;
        Debug.Log("GameManager: All game data reset.");
    }
}