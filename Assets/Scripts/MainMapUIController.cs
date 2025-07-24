using UnityEngine;
using TMPro;
using System.Collections;

public class MainMapUIController : MonoBehaviour
{
    public TextMeshProUGUI miniGameResultText;
    public TextMeshProUGUI highestScoreText;

    public GameObject miniGameResultPanel; 

    private void Awake()
    {
        if (miniGameResultPanel != null)
        {
            miniGameResultPanel.SetActive(false);
        }
    }

    private void Start()
    {
        if (miniGameResultText == null)
        {
            Debug.LogError("MainMapUIController: MiniGameResultText UI is not assigned! Please assign it in the Inspector.");
        }
        if (highestScoreText == null)
        {
            Debug.LogError("MainMapUIController: HighestScoreText UI is not assigned! Please assign it in the Inspector.");
        }

        if (GameManager.Instance != null)
        {
            DisplayHighestScore();
        }
        else 
        {
            Debug.LogWarning("[MainMapUIController] GameManager instance not found. Skipping mini-game result display.");
            if (miniGameResultText != null) miniGameResultText.text = "";
            if (highestScoreText != null) highestScoreText.text = "Highest Score: N/A";
            if (miniGameResultPanel != null) miniGameResultPanel.SetActive(false);
        }
    }

    public void ShowMiniGameResultUI()
    {
        if (miniGameResultPanel != null)
        {
            miniGameResultPanel.SetActive(true); 
        }
        DisplayMiniGameResult(); 
    }

    private void DisplayMiniGameResult()
    {
        if (miniGameResultText == null) return;

        string resultMessage = "";

        if (GameManager.Instance.didMiniGameSucceed)
        {
            resultMessage = $"MINI-GAME CLEARED! Score: {GameManager.Instance.currentMiniGameScore}";
        }
        else
        {
            resultMessage = $"MINI-GAME FAILED! Score: {GameManager.Instance.currentMiniGameScore}";
        }
        miniGameResultText.text = resultMessage;
    }

    private void DisplayHighestScore()
    {
        if (highestScoreText == null) return;
        highestScoreText.text = $"HIGHEST SCORE: {GameManager.Instance.highestMiniGameScore}";
    }

    public void HideResultAfterDelay(float delay)
    {
        StartCoroutine(ClearMessageAfterDelay(delay));
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (miniGameResultPanel != null)
        {
            miniGameResultPanel.SetActive(false); 
        }
        if (miniGameResultText != null)
        {
            miniGameResultText.text = ""; 
        }
    }
}