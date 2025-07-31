using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BgLooper : MonoBehaviour
{
    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        ObstacleBase[] obstacles = GameObject.FindObjectsOfType<ObstacleBase>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered: " + collision.name);

        ObstacleBase obstacle = collision.GetComponent<ObstacleBase>();        
    }
}