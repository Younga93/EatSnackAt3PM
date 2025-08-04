using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    // TODO: 플레이어 컴포넌트를 매개변수로 받아야함
    void OnInteract(PlayerController player);
}
