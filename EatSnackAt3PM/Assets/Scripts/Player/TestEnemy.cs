using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision Enter in Enemy : {collision.gameObject.name}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Trigger Enter in Enemy : {collision.gameObject.name}");
    }
}
