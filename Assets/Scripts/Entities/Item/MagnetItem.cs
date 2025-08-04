using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : ItemBase
{
    [SerializeField] private float _duration;

    public override void OnInteract(PlayerController player)
    {
        MagnetArea area = player.transform.parent.GetComponentInChildren<MagnetArea>(true);
        area.gameObject.SetActive(true);
        area.Init(_duration, player);

        GameManager.Instance.SpawnManager.ReturnItem(this);
    }

    IEnumerator AttractItemsCoroutine(PlayerController player)
    {
        yield return new WaitForSeconds(_duration);
        player.GetComponentInChildren<MagnetArea>().gameObject.SetActive(false);
    }
}
