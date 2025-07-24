using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator animator = null;
    Rigidbody2D _rigidbody = null;

    public float flapForce = 6f;
    public float forwardSpeed = 3f;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isFlap = false;

    public bool godMode = false;

    FlappyPlaneGameManager gameManager = null; 

    void Start()
    {
        gameManager = FlappyPlaneGameManager.Instance;

        animator = transform.GetComponentInChildren<Animator>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            Debug.LogError("Player 스크립트: Animator를 찾지 못했습니다.");
        }

        if (_rigidbody == null)
        {
            Debug.LogError("Player 스크립트: Rigidbody2D를 찾지 못했습니다.");
        }
    }

    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    if (gameManager != null)
                    {
                        gameManager.GameOver(false); 
                    }
                    else
                    {
                        Debug.LogWarning("FlappyPlaneGameManager 인스턴스를 찾을 수 없습니다. (Player 스크립트)");
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
            }
            else 
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true; 
            }
        }
    }

    public void FixedUpdate()
    {
        if (isDead)
            return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed; 

 
        if (isFlap)
        {
            velocity.y = 0; 
            velocity.y += flapForce;
            isFlap = false; 
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

 
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode)
            return;

        if (isDead)
            return;

        animator.SetInteger("IsDie", 1); 
        isDead = true; 
        deathCooldown = 1f; 

        if (gameManager != null)
        {
            gameManager.GameOver(false); 
        }
        else
        {
            Debug.LogWarning("FlappyPlaneGameManager 인스턴스를 찾을 수 없습니다. (Player 스크립트 - OnCollision)");
        }
    }
}