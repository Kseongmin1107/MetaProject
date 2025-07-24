using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스 패턴
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [Header("Scene Settings")]
    public string mainMapSceneName = "MetaverseMainScene"; // 메인 맵 씬 이름
    public string miniGameSceneName = "FlappyPlaneScene"; // 미니게임 씬 이름

    [Header("Mini-Game Data")]
    public int currentMiniGameScore = 0; // 현재 미니게임에서 획득한 점수
    public int highestMiniGameScore = 0; // 저장된 최고 점수
    public bool didMiniGameSucceed = false; // 미니게임 성공 여부

    private MainMapUIController mainMapUIController;

    private void Awake()
    {
        // 싱글톤 패턴 구현: 이미 인스턴스가 있다면 이 오브젝트 파괴
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

    // 미니게임으로 진입할 때 호출 (예: 메인 맵에서 미니게임 트리거에 닿았을 때)
    public void StartMiniGame()
    {
        Debug.Log("GameManager: Starting Mini-Game.");
        currentMiniGameScore = 0; // 미니게임 시작 시 점수 초기화
        didMiniGameSucceed = false; // 미니게임 성공 여부 초기화
        SceneManager.LoadScene(miniGameSceneName); // 미니게임 씬 로드
    }

    public void EndMiniGame()
    {
        Debug.Log("GameManager: Ending Mini-Game. Returning to Main Map.");
        // 최고 점수 갱신 및 저장
        CheckAndSaveHighestScore(currentMiniGameScore);

        SceneManager.LoadScene(mainMapSceneName); // 메인 맵 씬 로드

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

    // 현재 미니게임 점수를 설정
    public void SetMiniGameScore(int score)
    {
        currentMiniGameScore = score;
        Debug.Log($"GameManager: Mini-Game Score set to {currentMiniGameScore}");
    }

    // 미니게임 성공 여부를 설정
    public void SetMiniGameResult(bool success)
    {
        didMiniGameSucceed = success;
        Debug.Log($"GameManager: Mini-Game Result: {(success ? "SUCCESS" : "FAILED")}");
    }

    // 최고 점수 확인 및 저장
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
        highestMiniGameScore = PlayerPrefs.GetInt("HighestMiniGameScore", 0); // "HighestMiniGameScore" 키로 저장된 값을 로드, 없으면 0
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