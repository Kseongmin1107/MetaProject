using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // �÷��̾� WASD �̵� �ӵ�
    public float mouseMoveSpeed = 7f; // ���콺 Ŭ�� �̵� �ӵ� (WASD���� ������ ������ �� ����)
    public float stoppingDistance = 0.1f; // ���콺 Ŭ�� �̵� �� ��ǥ ������ ���� �����ߴٰ� �Ǵ��ϴ� �Ÿ�

    private Rigidbody2D rb;
    private Vector2 currentMoveInput; // WASD �Է��� ����
    private Vector3 targetMousePosition; // ���콺 Ŭ�� ��ǥ ��ġ
    private bool isMovingToMouseClick = false; // ���콺 Ŭ������ �̵� ������ ����

    
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4.5f;
    public float maxY = 4.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("PlayerMove: Rigidbody2D is not found on Player! Please add a Rigidbody2D component.");
        }
    }

    void Update()
    {
        
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }

        currentMoveInput = new Vector2(moveX, moveY).normalized;

        
        if (currentMoveInput.magnitude > 0)
        {
            isMovingToMouseClick = false;
        }

        
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 screenPos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            
            worldPos.z = 0;

            targetMousePosition = worldPos;
            isMovingToMouseClick = true; 

            
        }

       
        Vector2 actualMoveDirection = Vector2.zero;
        if (isMovingToMouseClick)
        {
            
            actualMoveDirection = (targetMousePosition - transform.position).normalized;
        }
        else 
        {
            actualMoveDirection = currentMoveInput;
        }

        if (actualMoveDirection.x > 0) 
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (actualMoveDirection.x < 0) 
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return; 

        if (isMovingToMouseClick)
        {
            
            Vector3 directionToTarget = targetMousePosition - transform.position;

            // ��ǥ ������ ���� �����ߴ��� Ȯ��
            if (directionToTarget.magnitude <= stoppingDistance)
            {
                rb.velocity = Vector2.zero; // �̵� ����
                isMovingToMouseClick = false; // ���콺 Ŭ�� �̵� ����
            }
            else
            {
                // ��ǥ ������ ���� �̵�
                rb.velocity = directionToTarget.normalized * mouseMoveSpeed;
            }
        }
        else // ���콺 Ŭ�� �̵� ���� �ƴ� ���� WASD �̵� ����
        {
            rb.velocity = currentMoveInput * moveSpeed;
        }
    }

    void LateUpdate()
    {
        
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}