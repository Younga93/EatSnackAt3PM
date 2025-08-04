using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour, IInteractable
{
    [SerializeField] private ObstacleType obstacleType;
    [SerializeField] protected AudioClip damagedClip;

    public abstract void OnInteract(PlayerController player);

    public ObstacleType GetObstacleType() { return obstacleType; }

}
