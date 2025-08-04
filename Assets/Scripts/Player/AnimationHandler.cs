using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;

    [Header("Jump Setting")]
    [SerializeField] float jumpHeight; // 이단 점프 측정할 높이
    [SerializeField] float resumeAnimationHeight; // 점프하고 내려올때 다시 애니메이션 재개할 기준 높이
    [SerializeField] float maxJumpHeight; // 최대 높이
    [SerializeField] float groundHeight;
    [SerializeField] float invincibleAlpha; // 무적일 때 투명도

    private readonly int IsJump = Animator.StringToHash("IsJump");
    private readonly int IsSlide = Animator.StringToHash("IsSlide");
    private readonly int IsAttack = Animator.StringToHash("IsAttack");
    private readonly int IsDamage = Animator.StringToHash("IsDamage");
    private readonly int IsDeath = Animator.StringToHash("IsDeath");
    

    private Rigidbody2D rb;
    private SpriteRenderer[] renderers;

    private Color originalColor;
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
        originalColor = new Color(1, 1, 1, 1);
    }
    private void FixedUpdate()
    {

        if (rb.velocity.y < 0f
            && animator.speed < 1f)
        {
            animator.speed = 1f;
        }

        // 최대 높이 설정
        Vector3 pos = transform.position;
        if (pos.y > maxJumpHeight) pos.y = maxJumpHeight;
        transform.position = pos;
    }


    public void Jump(int jumpCount)
    {

        if (transform.position.y > groundHeight)
        {
            Debug.Log("Double Jump Animation Stopped");
            animator.speed = 0f;
            // Invoke("ResumeAnimation", inAirStopTime);
        }
        else
        {
            animator.SetTrigger(IsJump);
        }
        
    }

    public void Slide()
    {
        animator.speed = 1f;
        animator.SetBool(IsSlide, true);
    }

    public void EndSlide()
    {
        animator.SetBool(IsSlide, false);
    }

    public void Attack()
    {
        animator.speed = 1f;
        animator.SetTrigger(IsAttack);
    }

    public void Damage()
    {
        animator.SetTrigger(IsDamage);
    }

    public void Death()
    {
        animator.SetBool(IsDeath, true);
    }

    public void StartInvincible()
    {
        foreach(var renderer in renderers)
        {
            Color color = renderer.color;
            renderer.color = new Color(color.r/2f, color.g / 2f, color.b / 2f, color.a);
        }
    }
    public void EndInvincible()
    {
        foreach (var renderer in renderers)
        {
            renderer.color = originalColor;
        }
    }
}
