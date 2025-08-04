using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalObstacle : ObstacleBase
{
    public override void OnInteract(PlayerController player)
    {
        // TODO: 플레이어의 체력 감소 및 감속
        if (player.IsInvincible) return;
        player.ChangeHp(-20); // 임시값
        player.CollideWithObstacle();
        SoundManager.PlayClip(damagedClip, false);
    }
}
