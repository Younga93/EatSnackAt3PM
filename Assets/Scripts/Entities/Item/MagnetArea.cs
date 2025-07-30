using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetArea : MonoBehaviour
{
    private float _duration;
    private float _radius;

    public void Init()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collision이 끌어 당길 수 있는 아이템이라면 끌어당기는 함수 실행

    }
}
