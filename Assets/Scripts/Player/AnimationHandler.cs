using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;

    bool jumpStopped = false;
    [SerializeField] float inAirStopTime;

    private readonly int IsJump = Animator.StringToHash("IsJump");
    private readonly int IsSlide = Animator.StringToHash("IsSlide");
    private readonly int IsAttack = Animator.StringToHash("IsAttack");
    private readonly int IsDamage = Animator.StringToHash("IsDamage");
    private readonly int IsDeath = Animator.StringToHash("IsDeath");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }


    public void Jump()
    {
        if (!animator.GetBool(IsJump))
        {
            animator.SetBool(IsAttack, false);
            animator.SetBool(IsSlide, false);
            animator.SetBool(IsJump, true);
        }
        else // 더블점프 상황
        {
            if (!jumpStopped && IsInAirAnimation())
            {
                Debug.Log("Double Jump Animation Stopped");
                animator.speed = 0f;
                Invoke("ResumeAnimation", inAirStopTime);
                jumpStopped = true;
            }
        }
    }

    bool IsInAirAnimation()
    {
        // 0번 : 기본 레이어
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Debug.Log($"공중에 있는가 : {stateInfo.IsName("Jump In Air") || stateInfo.IsName("Jump Start")}");
        return stateInfo.IsName("Jump In Air") || stateInfo.IsName("Jump Start");
    }


    void ResumeAnimation()
    {
        animator.speed = 1f;
    }


    public void OnLandAnimationEnd()
    {
        animator.SetBool(IsJump, false);
        jumpStopped = false;
    }

    public void Slide()
    {
        if (!animator.GetBool(IsSlide))
        {
            animator.SetBool(IsAttack, false);
            animator.SetBool(IsJump, false);
            animator.speed = 1f;
            animator.SetBool(IsSlide, true);
            
        }
    }

    public void OnSlideAnimationEnd()
    {
        animator.SetBool(IsSlide, false);
    }

    public void Attack()
    {
        if (animator.GetBool(IsJump)) return;
        if (!animator.GetBool(IsAttack))
        {
            animator.SetBool(IsSlide, false);
            animator.SetBool(IsJump, false);
            animator.speed = 1f;
            animator.SetBool(IsAttack, true);
        }
    }

    public void EndAttack()
    {
        animator.SetBool(IsAttack, false);
    }

    public void Damage()
    {
        animator.SetTrigger(IsDamage);
    }

    public void Death()
    {
        animator.SetTrigger(IsDeath);
    }
}
