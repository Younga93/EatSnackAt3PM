using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 점프 관련 변수들
    [Header("Jump Settings")]
    [SerializeField] float firstJumpForce;
    [SerializeField] float secondJumpForce;
    
    int jumpCount = 0; // 이단 점프 감지용
    float maxJumpDelay = 0.2f; // 이단 점프할 때 최소한으로 기다려야하는 시간
    float jumpDelay;
    //

    // 슬라이드 관련 변수들
    float originalColliderOffsetY;
    float originalColliderSizeY;
    float slideColliderOffsetY;
    float slideColliderSizeY;

    bool isSlide = false;
    //

    // 공격 관련 변수들
    [Header("Attack Settings")]
    [SerializeField] float attackTime;
    public GameObject attackPivot;
    AttackHandler attackHandler;

    bool isAttack = false;
    //

    AnimationHandler aniHandler;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        aniHandler = GetComponent<AnimationHandler>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        jumpDelay = maxJumpDelay;
        
    }

    private void Start()
    {
        attackHandler = attackPivot.GetComponent<AttackHandler>();

        // 슬라이드 시 콜라이더 크기 변경을 위해 저장됨
        originalColliderSizeY = boxCollider.size.y;
        originalColliderOffsetY = boxCollider.offset.y;
        slideColliderSizeY = originalColliderSizeY / 2f;
        slideColliderOffsetY = originalColliderOffsetY - 1.4f;
    }


    private void FixedUpdate()
    {
        Tick();
    }

    void Tick()
    {
        if(jumpCount != 0)
        {
            jumpDelay += Time.deltaTime;
        }
    }

    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            Debug.Log("Jump");
            if (jumpCount < 2)
            {
                if (jumpDelay >= maxJumpDelay)
                {
                    Vector3 velocity = rb.velocity;
                    velocity.y += jumpCount == 0 ? firstJumpForce : secondJumpForce;
                    rb.velocity = velocity;
                    
                    aniHandler.Jump();

                    jumpCount++;
                    jumpDelay = 0f;
                }
            }
            else
            {
                return;
            }
        }
    }

    void OnSlide(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if(!isSlide)
            {
                Debug.Log("Slide");
                isSlide = true;
                StartSlide();
            }
        }
    }

    void OnAttack(InputValue inputValue)
    {
        if(inputValue.isPressed)
        {
            if (jumpCount != 0) return;
            if (isSlide) isSlide = false;
            if (!isAttack)
            {
                Debug.Log("Attack");
                isAttack = true;
                StartAttack();
            }
            

        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            if(jumpCount != 0)
            {
                jumpCount = 0;
            }
        }
    }

    private void StartSlide()
    {
        // 점프 중이면 떨어뜨리기
        if(jumpCount != 0)
        {
            Vector3 velo = rb.velocity;
            velo.y -= jumpCount == 1 ? firstJumpForce : firstJumpForce + secondJumpForce;
            rb.velocity = velo;
        }
        if (isAttack) isAttack = false;

        boxCollider.size = new Vector2(boxCollider.size.x, slideColliderSizeY);
        boxCollider.offset = new Vector2(boxCollider.offset.x, slideColliderOffsetY);
        aniHandler.Slide();
    }

    private void OnSlideAnimationEnd()
    {
        boxCollider.size = new Vector2(boxCollider.size.x, originalColliderSizeY);
        boxCollider.offset = new Vector2(boxCollider.offset.x, originalColliderOffsetY);
        isSlide = false;
    }

    private void StartAttack()
    {
        attackPivot.SetActive(true);
        Debug.Log("True");
        aniHandler.Attack();
        Invoke("EndAttack", attackTime);
    }

    private void EndAttack()
    {
        attackPivot.SetActive(false);
        aniHandler.EndAttack();
        isAttack = false;
    }
}
