using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 WASD 이동 속도
    public float mouseMoveSpeed = 7f; // 마우스 클릭 이동 속도 (WASD보다 빠르게 설정할 수 있음)
    public float stoppingDistance = 0.1f; // 마우스 클릭 이동 시 목표 지점에 거의 도달했다고 판단하는 거리

    private Rigidbody2D rb;
    private Vector2 currentMoveInput; // WASD 입력을 저장
    private Vector3 targetMousePosition; // 마우스 클릭 목표 위치
    private bool isMovingToMouseClick = false; // 마우스 클릭으로 이동 중인지 여부

    
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

            // 목표 지점에 거의 도달했는지 확인
            if (directionToTarget.magnitude <= stoppingDistance)
            {
                rb.velocity = Vector2.zero; // 이동 중지
                isMovingToMouseClick = false; // 마우스 클릭 이동 종료
            }
            else
            {
                // 목표 지점을 향해 이동
                rb.velocity = directionToTarget.normalized * mouseMoveSpeed;
            }
        }
        else // 마우스 클릭 이동 중이 아닐 때만 WASD 이동 적용
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