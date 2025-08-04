using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    PlayerController playerController;
    private float offsetX;

    public void Init(PlayerController playerController)
    {
        this.playerController = playerController;   
        offsetX = transform.position.x - playerController.transform.position.x;
    }

    private void FixedUpdate()
    {
        Vector3 position = playerController.transform.position;
        position.x += offsetX;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BreakableObstacle>()?.Break(playerController);
        }
    }

    public void StopAttack()
    {
        gameObject.SetActive(false);
        playerController.IsAttack = false;
    }
}
