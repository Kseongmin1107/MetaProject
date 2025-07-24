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
                Debug.Log($"[MiniGameZoneTrigger] 플레이어가 {gameObject.name} (미니게임 존)에 진입했습니다!");
            }
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartMiniGame();  
            }
            else
            {
                SceneManager.LoadScene(miniGameSceneName); 
                Debug.LogWarning("[MiniGameZoneTrigger] GameManager 인스턴스를 찾을 수 없습니다. 직접 씬을 로드합니다.");
            }
        }
    }

    private void Start()
    {
        
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError($"[MiniGameZoneTrigger] 에러: {gameObject.name} 오브젝트에 Collider2D 컴포넌트가 없습니다! 트리거가 작동하지 않습니다.");
            enabled = false;
            return;
        }
      
        if (!GetComponent<Collider2D>().isTrigger)
        {
            Debug.LogWarning($"[MiniGameZoneTrigger] 경고: {gameObject.name} 오브젝트의 Collider2D가 'Is Trigger'로 설정되어 있지 않습니다. 트리거 이벤트가 발생하지 않을 수 있습니다.");
        }
    
        if (string.IsNullOrEmpty(miniGameSceneName))
        {
            Debug.LogError($"[MiniGameZoneTrigger] 에러: {gameObject.name} 오브젝트의 'Mini Game Scene Name'이 설정되지 않았습니다! Inspector에서 정확한 씬 이름을 입력해주세요.");
            enabled = false; 
            return;
        }
    }
}