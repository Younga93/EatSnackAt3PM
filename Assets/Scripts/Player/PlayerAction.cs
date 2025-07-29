using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private PlayerController playerController;

    private Dictionary<PlayerActionState, BaseAction> actionDict;
    // TODO : Dictionary에 연결할 액션들 구현

    Animator animator;

    public void Init(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        actionDict[playerController.state].PlayAction();
    }
}
