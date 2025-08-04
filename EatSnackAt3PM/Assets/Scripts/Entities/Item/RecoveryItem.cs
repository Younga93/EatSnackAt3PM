using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItem : ItemBase
{
    [SerializeField] private int _energyRecoveryAmount;

    public override void OnInteract(PlayerController player)
    {
        // TODO: 플레이어 체력 회복
        player.ChangeHp(_energyRecoveryAmount);
        GameManager.Instance.SpawnManager.ReturnItem(this);
    }
}
