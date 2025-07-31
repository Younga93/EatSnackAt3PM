using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : ItemBase
{
    [SerializeField] private float _duration;

    public override void OnInteract(PlayerController player)
    {
        StartCoroutine(AttractItemsCoroutine(player));
    }

    IEnumerator AttractItemsCoroutine(PlayerController player)
    {
        player.GetComponentInChildren<MagnetArea>(true).gameObject.SetActive(true);
        yield return new WaitForSeconds(_duration);
        player.GetComponentInChildren<MagnetArea>().gameObject.SetActive(false);
    }
}
