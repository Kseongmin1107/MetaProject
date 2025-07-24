using UnityEngine;
using TMPro;
using System.Collections; // IEnumerator 사용을 위해 추가

public class MainMapUIController : MonoBehaviour
{
    public TextMeshProUGUI miniGameResultText;
    public TextMeshProUGUI highestScoreText;

    // 미니게임 결과 메시지를 포함하는 UI 패널 GameObject
    // 이 패널을 활성화/비활성화하여 결과 메시지를 숨기거나 보이게 합니다.
    public GameObject miniGameResultPanel; // Inspector에서 연결 필요

    private void Awake()
    {
        // 게임 시작 시, 결과 패널을 기본적으로 비활성화(숨김)
        // 이렇게 하면 메인 맵 로드 시 "MINI-GAME FAILED!" 메시지가 즉시 뜨지 않습니다.
        if (miniGameResultPanel != null)
        {
            miniGameResultPanel.SetActive(false);
        }
    }

    private void Start()
    {
        // UI TextMeshProUGUI 컴포넌트가 Inspector에 할당되었는지 확인
        if (miniGameResultText == null)
        {
            Debug.LogError("MainMapUIController: MiniGameResultText UI is not assigned! Please assign it in the Inspector.");
        }
        if (highestScoreText == null)
        {
            Debug.LogError("MainMapUIController: HighestScoreText UI is not assigned! Please assign it in the Inspector.");
        }

        // GameManager 인스턴스가 존재하는 경우
        if (GameManager.Instance != null)
        {
            // 최고 점수는 게임 시작 시 항상 표시합니다.
            DisplayHighestScore();

            // 중요: Start()에서는 DisplayMiniGameResult()를 직접 호출하지 않습니다.
            // 미니게임 결과는 ShowMiniGameResultUI() 함수가 호출될 때만 표시됩니다.
        }
        else // GameManager 인스턴스를 찾을 수 없는 경우 (예: 게임을 메인 맵 씬부터 직접 실행했을 때)
        {
            Debug.LogWarning("[MainMapUIController] GameManager instance not found. Skipping mini-game result display.");
            // UI 텍스트들을 초기화
            if (miniGameResultText != null) miniGameResultText.text = "";
            if (highestScoreText != null) highestScoreText.text = "Highest Score: N/A";
            // 결과 패널도 비활성화합니다.
            if (miniGameResultPanel != null) miniGameResultPanel.SetActive(false);
        }
    }

    // 이 함수는 GameManager에서 미니게임이 끝난 후 메인 맵으로 돌아올 때 호출됩니다.
    public void ShowMiniGameResultUI()
    {
        if (miniGameResultPanel != null)
        {
            miniGameResultPanel.SetActive(true); // 결과 패널을 활성화하여 보이게 합니다.
        }
        DisplayMiniGameResult(); // 미니게임 결과를 텍스트에 업데이트합니다.
    }

    // 현재 미니게임의 결과를 TextMeshProUGUI에 표시하는 내부 함수
    private void DisplayMiniGameResult()
    {
        if (miniGameResultText == null) return;

        string resultMessage = "";
        // GameManager에서 미니게임 성공 여부에 따라 메시지 설정
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

    // 최고 점수를 TextMeshProUGUI에 표시하는 내부 함수
    private void DisplayHighestScore()
    {
        if (highestScoreText == null) return;
        highestScoreText.text = $"HIGHEST SCORE: {GameManager.Instance.highestMiniGameScore}";
    }

    // (선택 사항) 결과 메시지를 일정 시간 후 자동으로 숨기는 코루틴
    // GameManager에서 필요에 따라 호출할 수 있습니다.
    public void HideResultAfterDelay(float delay)
    {
        StartCoroutine(ClearMessageAfterDelay(delay));
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기
        if (miniGameResultPanel != null)
        {
            miniGameResultPanel.SetActive(false); // 결과 패널 비활성화
        }
        if (miniGameResultText != null)
        {
            miniGameResultText.text = ""; // 결과 텍스트를 비웁니다.
        }
    }
}