using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObstacle : ObstacleBase
{
    [SerializeField] private float _energyRecoveryAmount;
    [SerializeField] private int _score;

    public override void OnInteract()
    {
        // TODO: 플레이어의 체력 감소 및 감속
    }

    public void Break()
    {
        // TODO: 오브젝트 파괴 및 플레이어 체력 회복, 점수 증가?
    }
}
