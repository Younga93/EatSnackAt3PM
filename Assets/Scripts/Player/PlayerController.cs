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
    [SerializeField] float maxForwardSpeed; // 최대 속도


    // 점프 관련 변수들
    [Header("Jump Settings")]
    [SerializeField] float firstJumpForce;
    [SerializeField] float secondJumpForce;
    
    int jumpCount = 0; // 이단 점프 감지용
    float maxJumpDelay = 0.2f; // 이단 점프할 때 최소한으로 기다려야하는 시간
    float jumpDelay;
    //

    // 슬라이드 관련 변수들
    [Header("Slide Settings")]
    [SerializeField] private InputActionReference slideAction;
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
    public bool IsAttack { get => isAttack; set => isAttack = value; }
    //

    // 무적 관련 변수들
    [Header("Invincible Settings")]
    [SerializeField] float invincibleTime;

    bool isInvincible = false;
    public bool IsInvincible { get { return isInvincible; } }
    //

    // 효과음 관련 변수들
    [Header("Sound Settings")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip slideSound;
    [SerializeField] AudioClip attackSound;
    // TODO : 데미지 받으면 재생되는 사운드 추가
    //[SerializeField] AudioClip damageSound;

    private AudioSource slideSource;
    //

    // input action 관련 변수들
    [Header("Input Settings")]
    [SerializeField] InputActionAsset inputActions;
    private InputAction attackAction;


    // 틱 관련 변수
    float tick = 0f;

    // 컴포넌트들 
    AnimationHandler aniHandler;
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    PlayerInput playerInput;

    private void Awake()
    {
        aniHandler = GetComponent<AnimationHandler>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        jumpDelay = maxJumpDelay;

        playerInput = GetComponent<PlayerInput>();
        attackAction = inputActions.FindActionMap("Player").FindAction("Attack");
    }


    private void OnDisable()
    {
        slideAction.action.started -= StartSliding;
        slideAction.action.canceled -= StopSliding;
        slideAction.action.Disable();
    }

    private void Start()
    {
        attackHandler = attackPivot.GetComponent<AttackHandler>();
        attackHandler.Init(this);

        // 슬라이드 시 콜라이더 크기 변경을 위해 저장됨
        originalColliderSizeY = capsuleCollider.size.y;
        originalColliderOffsetY = capsuleCollider.offset.y;
        slideColliderSizeY = originalColliderSizeY / 2f;
        slideColliderOffsetY = originalColliderOffsetY - 1.4f;
        SetHp(maxHp);

        // 슬라이드 액션 설정
        slideAction.action.started += StartSliding;
        slideAction.action.canceled += StopSliding;
        slideAction.action.Enable();

         
    }


    private void FixedUpdate()
    {
        Tick();

        Vector3 velo = rb.velocity;
        velo.x = forwardSpeed;
        rb.velocity = velo;

        if(tick >= 1.0f)
        {
            // 1초에 한번씩 실행됨
            ChangeHp(-1);
            if(forwardSpeed < maxForwardSpeed) forwardSpeed += 0.1f;
            tick = 0f;
        }

        // if (slideAction.action.IsPressed()) StartSliding();
    }
    public void StartSliding(InputAction.CallbackContext context)
    {
        // Debug.Log("Slide");
        if (slideSource == null) slideSource = SoundManager.PlayClip(slideSound, true);
        isSlide = true;
        StartSlide();
        jumpCount = 0;
        
    }

    public void StopSliding(InputAction.CallbackContext context)
    {
        Debug.Log("Stop Slide");
        isSlide = false;
        SlideEnd();
        if (slideSource != null)
        {
            slideSource.gameObject.GetComponent<SoundSource>()?.Disable();
            slideSource = null;
        }
    }

    void Tick()
    {
        if(jumpDelay < maxJumpDelay)
        {
            jumpDelay += Time.deltaTime;
        }
        tick += Time.deltaTime;
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
                    SlideEnd();

                    Vector3 velocity = rb.velocity;
                    velocity.y += jumpCount == 0 ? firstJumpForce : secondJumpForce;
                    rb.velocity = velocity;
                    
                    aniHandler.Jump(jumpCount);

                    jumpCount++;
                    jumpDelay = 0f;
                    SoundManager.PlayClip(jumpSound, false);
                }
            }
            else
            {
                return;
            }
        }
    }
    void OnAttack(InputValue inputValue)
    {
        if(inputValue.isPressed)
        {
            if (isSlide) {
                SlideEnd();
            }
            if (!isAttack)
            {
                Debug.Log("Attack");
                isAttack = true;
                StartAttack();
                SoundManager.PlayClip(attackSound, false);
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

    public void GetDamaged()
    {
        //if(changeHp < 0)
        //{
        //    SoundManager.PlayClip(damageSound);
        //}
    }

    /// <summary>
    /// 체력 처리 함수. 체력을 바꿉니다.
    /// </summary>
    /// <param name="changeHp"></param>
    public void ChangeHp(int changeHp)
    {
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

        capsuleCollider.size = new Vector2(capsuleCollider.size.x, slideColliderSizeY);
        capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, slideColliderOffsetY);
        aniHandler.Slide();
    }

    private void SlideEnd()
    {
        capsuleCollider.size = new Vector2(capsuleCollider.size.x, originalColliderSizeY);
        capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, originalColliderOffsetY);
        isSlide = false;
        aniHandler.EndSlide();
    }

    private void StartAttack()
    {
        attackPivot.SetActive(true);
        Debug.Log("True");
        aniHandler.Attack();
        // Invoke("EndAttack", attackTime);
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

    // 아이템 써서 무적 시작부분
    public void StartInvincible(float? itemInvincibleTime)
    {      
        CancelInvoke("EndInvincible");
        if (!isInvincible) aniHandler.StartInvincible();
        isInvincible = true;
        if( itemInvincibleTime == null)
        {
            attackAction.Disable();
            Invoke("EndInvincible", invincibleTime);
        }
        else
        {
            Invoke("EndInvincible", (float)itemInvincibleTime);
        }
    }

    public void EndInvincible()
    {
        // 무적 끝나는 부분
        // Debug.Log("End Invincible");
        attackAction.Enable();
        isInvincible = false;
        aniHandler.EndInvincible();
        GameManager.Instance.ApplyEquippedOutfitItems();
    }

    public void Death()
    {
        // 죽는 부분
        aniHandler.Death();
    }
}
