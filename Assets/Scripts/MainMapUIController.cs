using UnityEngine;
using TMPro;
using System.Collections; // IEnumerator ����� ���� �߰�

public class MainMapUIController : MonoBehaviour
{
    public TextMeshProUGUI miniGameResultText;
    public TextMeshProUGUI highestScoreText;

    // �̴ϰ��� ��� �޽����� �����ϴ� UI �г� GameObject
    // �� �г��� Ȱ��ȭ/��Ȱ��ȭ�Ͽ� ��� �޽����� ����ų� ���̰� �մϴ�.
    public GameObject miniGameResultPanel; // Inspector���� ���� �ʿ�

    private void Awake()
    {
        // ���� ���� ��, ��� �г��� �⺻������ ��Ȱ��ȭ(����)
        // �̷��� �ϸ� ���� �� �ε� �� "MINI-GAME FAILED!" �޽����� ��� ���� �ʽ��ϴ�.
        if (miniGameResultPanel != null)
        {
            miniGameResultPanel.SetActive(false);
        }
    }

    private void Start()
    {
        // UI TextMeshProUGUI ������Ʈ�� Inspector�� �Ҵ�Ǿ����� Ȯ��
        if (miniGameResultText == null)
        {
            Debug.LogError("MainMapUIController: MiniGameResultText UI is not assigned! Please assign it in the Inspector.");
        }
        if (highestScoreText == null)
        {
            Debug.LogError("MainMapUIController: HighestScoreText UI is not assigned! Please assign it in the Inspector.");
        }

        // GameManager �ν��Ͻ��� �����ϴ� ���
        if (GameManager.Instance != null)
        {
            // �ְ� ������ ���� ���� �� �׻� ǥ���մϴ�.
            DisplayHighestScore();

            // �߿�: Start()������ DisplayMiniGameResult()�� ���� ȣ������ �ʽ��ϴ�.
            // �̴ϰ��� ����� ShowMiniGameResultUI() �Լ��� ȣ��� ���� ǥ�õ˴ϴ�.
        }
        else // GameManager �ν��Ͻ��� ã�� �� ���� ��� (��: ������ ���� �� ������ ���� �������� ��)
        {
            Debug.LogWarning("[MainMapUIController] GameManager instance not found. Skipping mini-game result display.");
            // UI �ؽ�Ʈ���� �ʱ�ȭ
            if (miniGameResultText != null) miniGameResultText.text = "";
            if (highestScoreText != null) highestScoreText.text = "Highest Score: N/A";
            // ��� �гε� ��Ȱ��ȭ�մϴ�.
            if (miniGameResultPanel != null) miniGameResultPanel.SetActive(false);
        }
    }

    // �� �Լ��� GameManager���� �̴ϰ����� ���� �� ���� ������ ���ƿ� �� ȣ��˴ϴ�.
    public void ShowMiniGameResultUI()
    {
        if (miniGameResultPanel != null)
        {
            miniGameResultPanel.SetActive(true); // ��� �г��� Ȱ��ȭ�Ͽ� ���̰� �մϴ�.
        }
        DisplayMiniGameResult(); // �̴ϰ��� ����� �ؽ�Ʈ�� ������Ʈ�մϴ�.
    }

    // ���� �̴ϰ����� ����� TextMeshProUGUI�� ǥ���ϴ� ���� �Լ�
    private void DisplayMiniGameResult()
    {
        if (miniGameResultText == null) return;

        string resultMessage = "";
        // GameManager���� �̴ϰ��� ���� ���ο� ���� �޽��� ����
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

    // �ְ� ������ TextMeshProUGUI�� ǥ���ϴ� ���� �Լ�
    private void DisplayHighestScore()
    {
        if (highestScoreText == null) return;
        highestScoreText.text = $"HIGHEST SCORE: {GameManager.Instance.highestMiniGameScore}";
    }

    // (���� ����) ��� �޽����� ���� �ð� �� �ڵ����� ����� �ڷ�ƾ
    // GameManager���� �ʿ信 ���� ȣ���� �� �ֽ��ϴ�.
    public void HideResultAfterDelay(float delay)
    {
        StartCoroutine(ClearMessageAfterDelay(delay));
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ������ �ð���ŭ ���
        if (miniGameResultPanel != null)
        {
            miniGameResultPanel.SetActive(false); // ��� �г� ��Ȱ��ȭ
        }
        if (miniGameResultText != null)
        {
            miniGameResultText.text = ""; // ��� �ؽ�Ʈ�� ���ϴ�.
        }
    }
}