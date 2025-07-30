﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : ItemBase
{
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;

    public override void OnInteract()
    {
        // TODO: 플레이어 속도를 일시적으로 상승 시킴, 코루틴으로 구현할 듯?
    }

    // 매개변수로 플레이어 속도 조절 가능한 스크립트 받아옴
    IEnumerator PlayerSpeedUpCoroutine()
    {
        // 속도 증가
        yield return new WaitForSeconds(_duration);
        // 속도 감소
    }
}
