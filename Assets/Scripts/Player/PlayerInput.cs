using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController playerController;
    public void Init(PlayerController playerController)
    {
        this.playerController = playerController;
    }


    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            // TODO : State 바꾸기
        }
    }

    void OnSlide(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            // TODO : State 바꾸기
        }
    }

    void OnAttack(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            // TODO : State 바꾸기
        }
    }
}
