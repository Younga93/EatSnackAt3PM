using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleItem : ItemBase
{
    [SerializeField] private float _duration;

    public override void OnInteract(PlayerController player)
    {
        player.StartInvincible(_duration);
    }
}
