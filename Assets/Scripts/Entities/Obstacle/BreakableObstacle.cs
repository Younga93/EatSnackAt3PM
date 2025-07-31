using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObstacle : ObstacleBase
{
    [SerializeField] private int _energyRecoveryAmount;
    [SerializeField] private int _score;

    public override void OnInteract(PlayerController player)
    {
        // TODO: 플레이어의 체력 감소 및 감속
        player.ChangeHp(-20); // 임시값
    }

    public void Break(PlayerController player)
    {
        player.ChangeHp(_energyRecoveryAmount);
        GameManager.Instance.AddScore(_score);
        PresetSpawnManager.Instance.ReturnObstacle(this);
        // TODO: 파괴 이펙트같은게 있으면 좋을 거 같음
    }
}
