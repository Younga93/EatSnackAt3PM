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

    private List<int> animatorParams = new List<int>();



    private void Awake()
    {
        animator = GetComponent<Animator>();
        animatorParams.Add(IsJump);
        animatorParams.Add(IsSlide);
        animatorParams.Add(IsAttack);
        animatorParams.Add(IsDamage);
        animatorParams.Add(IsDeath);
    }

    private void Update()
    {
        
    }


    public void Jump()
    {
        if (!animator.GetBool(IsJump))
        {
            TurnOnState(IsJump);
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
        return stateInfo.IsName("Jump");
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
            TurnOnState(IsSlide);
            animator.speed = 1f;
            
            
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
            animator.speed = 1f;
            TurnOnState(IsAttack);
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


    /// <summary>
    /// state 하나만 키고 나머지는 다 false 처리
    /// </summary>
    /// <param name="state"></param>
    public void TurnOnState(int state) 
    {
        foreach(var aniParam in animatorParams)
        {
            if(aniParam == state)
            {
                animator.SetBool(state, true);
            }
            else
            {
                animator.SetBool(aniParam, false);
            }
        }
    }

    /// <summary>
    /// 모든 파라미터를 false 처리
    /// </summary>
    public void TurnOffAllState()
    {
        foreach(var aniParam in animatorParams)
        {
            animator.SetBool(aniParam, false);
        }
    }
}
