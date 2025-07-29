using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerActionState
{
    Idle,
    Jump,
    DoubleJump,
    Slide,
    Attack,
    Damage,
    Death
}

public class PlayerController : MonoBehaviour
{

    public PlayerActionState state = PlayerActionState.Idle;

    private PlayerInput playerInput;
    private PlayerAction playerAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.Init(this);
        playerAction = GetComponent<PlayerAction>();
        playerAction.Init(this);
    }

}
