using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Vector3 playerDistance;

    [SerializeField] Transform player;

    // Start is called before the first frame update
    void Start()
    {
        playerDistance = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = player.position.x + playerDistance.x;
        transform.position = position;
    }
}
