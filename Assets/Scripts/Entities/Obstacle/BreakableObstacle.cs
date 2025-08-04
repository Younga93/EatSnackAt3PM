using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObstacle : ObstacleBase
{
    [SerializeField] private int _energyRecoveryAmount;
    [SerializeField] private int _score;
    [SerializeField] private AudioClip breakClip;

    public override void OnInteract(PlayerController player)
    {
        // TODO: 플레이어의 체력 감소 및 감속
        if (player.IsInvincible) return;
        player.ChangeHp(-20); // 임시값
        player.CollideWithObstacle();
        SoundManager.PlayClip(damagedClip, false);
    }

    public void Break(PlayerController player)
    {
        player.ChangeHp(_energyRecoveryAmount);
        GameManager.Instance.AddScore(_score);
        GameManager.Instance.SpawnManager.ReturnObstacle(this);
        SoundManager.PlayClip(breakClip, false);
        // TODO: 파괴 이펙트같은게 있으면 좋을 거 같음
    }
}
