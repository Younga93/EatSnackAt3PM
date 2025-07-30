using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : ItemBase
{
    [SerializeField] private float _duration;
    [SerializeField] private float _range;

    public override void OnInteract(PlayerController player)
    {
        
    }

    IEnumerator AttractItemsCoroutine()
    {
        GameObject go = new GameObject("MagnetZone");
        // 부모를 플레이어로 설정
        // 

        yield return new WaitForSeconds(_duration);
    }
}
