using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : ItemBase
{
    [SerializeField] private float _duration;

    public override void OnInteract(PlayerController player)
    {
        MagnetArea area = player.GetComponentInChildren<MagnetArea>(true);
        area.gameObject.SetActive(true);
        area.Init(_duration);

        PresetSpawnManager.Instance.ReturnItem(this);
    }

    IEnumerator AttractItemsCoroutine(PlayerController player)
    {
        yield return new WaitForSeconds(_duration);
        player.GetComponentInChildren<MagnetArea>().gameObject.SetActive(false);
    }
}
