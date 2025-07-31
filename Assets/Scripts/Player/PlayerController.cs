using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 체력
    [Header("Health Settings")]
    [SerializeField] int maxHp = 100;
    public int currentHp; // 추후에 매니저쪽에서 접근하게 하려고 public으로 뒀습니다.

    // 이동 관련 변수
    [Header("Move Settings")]
    [SerializeField] float forwardSpeed; // 앞으로 이동하는 속도


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

    // 무적 관련 변수들
    [Header("Invincible Settings")]
    [SerializeField] float invincibleTime;

    bool isInvincible = false;
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
        SetHp(maxHp);
    }


    private void FixedUpdate()
    {
        Tick();

        Vector3 velo = rb.velocity;
        velo.x = forwardSpeed;
        rb.velocity = velo;
    }

    void Tick()
    {
        if(jumpDelay < maxJumpDelay)
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
                    OnSlideAnimationEnd();

                    Vector3 velocity = rb.velocity;
                    velocity.y += jumpCount == 0 ? firstJumpForce : secondJumpForce;
                    rb.velocity = velocity;
                    
                    aniHandler.Jump(jumpCount);

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
                jumpCount = 0;
            }
        }
    }

    void OnAttack(InputValue inputValue)
    {
        if(inputValue.isPressed)
        {
            if (isSlide) {
                OnSlideAnimationEnd();
            }
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

        Debug.Log($"Collision Enter in Player : {collision.gameObject.name}");

        if (collision.gameObject.CompareTag("BackGround"))
        {
            if(jumpCount != 0)
            {
                jumpCount = 0;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Trigger Enter in Player : {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.GetComponent<IInteractable>()?.OnInteract(this);   
        }
    }

    /// <summary>
    /// 체력 처리 함수. 체력을 바꿉니다.
    /// </summary>
    /// <param name="changeHp"></param>
    public void ChangeHp(int changeHp)
    {
        // 추후에 체력 변화치가 양수면 주변에 밝은 파티클이 돌아다녀도 괜찮을 것 같아요.
        currentHp += changeHp;
        currentHp = currentHp > maxHp? maxHp : currentHp;
        currentHp = currentHp < 0 ? 0 : currentHp;
        GameManager.Instance.UpdateHealth(currentHp);
    }

    /// <summary>
    /// 체력 처리 함수. 체력을 지정합니다.
    /// </summary>
    /// <param name="changeHp"></param>
    public void SetHp(int changeHp)
    {
        currentHp = changeHp;
        GameManager.Instance.UpdateHealth(currentHp);
    }


    /// <summary>
    /// 속도 처리 함수.
    /// 앞으로 달리는 속도를 바꿉니다
    /// </summary>
    /// <param name="changeSpeed"></param>

    public void ChangeSpeed(float changeSpeed)
    {
        forwardSpeed += changeSpeed;
    }


    /// <summary>
    /// 속도 처리 함수.
    /// 앞으로 달리는 속도를 지정합니다.
    /// </summary>
    /// <param name="changeSpeed"></param>
    public void SetSpeed(float changeSpeed)
    {
        forwardSpeed = changeSpeed;
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
        isAttack = false;
    }

    public void CollideWithObstacle()
    {
        if (isInvincible) return;
        StartInvincible(null);
        aniHandler.Damage();
    }

    public void StartInvincible(float? itemInvincibleTime)
    {
        // 무적 시작부분
        isInvincible = true;
        if (itemInvincibleTime == null)
            Invoke("EndInvincible", invincibleTime);
        else
            Invoke("EndInvincible", (float)itemInvincibleTime);
    }
    
    public void EndInvincible()
    {
        // 무적 끝나는 부분
        isInvincible = false;
    }

    public void Death()
    {
        // 죽는 부분
        aniHandler.Death();
    }
}
