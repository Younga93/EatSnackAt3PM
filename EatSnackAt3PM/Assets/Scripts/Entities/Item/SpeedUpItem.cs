using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : ItemBase
{
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;

    public override void OnInteract(PlayerController player)
    {
        // TODO: 플레이어 속도를 일시적으로 상승 시킴, 코루틴으로 구현할 듯?
        StartCoroutine(PlayerSpeedUpCoroutine(player));
        transform.parent = null;
        GetComponentInChildren<SpriteRenderer>().sprite = null;
    }

    // 매개변수로 플레이어 속도 조절 가능한 스크립트 받아옴
    // 현재로는 change로 하는데 SET으로 속도 정하는게 낫지 않나?
    IEnumerator PlayerSpeedUpCoroutine(PlayerController player)
    {
        player.ChangeSpeed(_speed);
        // 속도 증가
        yield return new WaitForSeconds(_duration);
        // 속도 감소
        player.ChangeSpeed(-_speed); // TODO: 플레이어 이전 속도로 되돌리기
        GameManager.Instance.SpawnManager.ReturnItem(this);
    }
}
