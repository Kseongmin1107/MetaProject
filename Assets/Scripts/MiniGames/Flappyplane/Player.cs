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
            Debug.LogError("Player ��ũ��Ʈ: Animator�� ã�� ���߽��ϴ�.");
        }

        if (_rigidbody == null)
        {
            Debug.LogError("Player ��ũ��Ʈ: Rigidbody2D�� ã�� ���߽��ϴ�.");
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
                        Debug.LogWarning("FlappyPlaneGameManager �ν��Ͻ��� ã�� �� �����ϴ�. (Player ��ũ��Ʈ)");
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
            Debug.LogWarning("FlappyPlaneGameManager �ν��Ͻ��� ã�� �� �����ϴ�. (Player ��ũ��Ʈ - OnCollision)");
        }
    }
}