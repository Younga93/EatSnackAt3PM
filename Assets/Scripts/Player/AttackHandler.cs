using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO : 장애물 부딛쳤을 때 판정 필요
        if (collision.CompareTag("enemy"))
        {
            Debug.Log("부딛힘");
        }
    }
}
