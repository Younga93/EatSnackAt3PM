using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : ItemBase
{
    // 각 코인 프리팹마다 상승 점수를 설정
    [SerializeField] private int _score;

    public override void OnInteract(PlayerController player)
    {
        // TODO: 게임매니저에 접근해서 점수 상승
    }
}
