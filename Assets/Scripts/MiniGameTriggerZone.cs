using UnityEngine;
using UnityEngine.SceneManagement; 

public class MiniGameZoneTrigger : MonoBehaviour
{
    public string miniGameSceneName = "FlappyPlaneScene"; 

    public bool enableDebugLog = true;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (enableDebugLog)
            {
                Debug.Log($"[MiniGameZoneTrigger] �÷��̾ {gameObject.name} (�̴ϰ��� ��)�� �����߽��ϴ�!");
            }
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartMiniGame();  
            }
            else
            {
                SceneManager.LoadScene(miniGameSceneName); 
                Debug.LogWarning("[MiniGameZoneTrigger] GameManager �ν��Ͻ��� ã�� �� �����ϴ�. ���� ���� �ε��մϴ�.");
            }
        }
    }

    private void Start()
    {
        
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError($"[MiniGameZoneTrigger] ����: {gameObject.name} ������Ʈ�� Collider2D ������Ʈ�� �����ϴ�! Ʈ���Ű� �۵����� �ʽ��ϴ�.");
            enabled = false;
            return;
        }
      
        if (!GetComponent<Collider2D>().isTrigger)
        {
            Debug.LogWarning($"[MiniGameZoneTrigger] ���: {gameObject.name} ������Ʈ�� Collider2D�� 'Is Trigger'�� �����Ǿ� ���� �ʽ��ϴ�. Ʈ���� �̺�Ʈ�� �߻����� ���� �� �ֽ��ϴ�.");
        }
    
        if (string.IsNullOrEmpty(miniGameSceneName))
        {
            Debug.LogError($"[MiniGameZoneTrigger] ����: {gameObject.name} ������Ʈ�� 'Mini Game Scene Name'�� �������� �ʾҽ��ϴ�! Inspector���� ��Ȯ�� �� �̸��� �Է����ּ���.");
            enabled = false; 
            return;
        }
    }
}