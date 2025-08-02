using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleItem : ItemBase
{
    [SerializeField] private float _duration;
    [SerializeField] private AudioClip invincibleClip;
    public override void OnInteract(PlayerController player)
    {
        player.StartInvincible(_duration);
        PresetSpawnManager.Instance.ReturnItem(this);
        SoundManager.PlayClip(invincibleClip, false);
    }
}
