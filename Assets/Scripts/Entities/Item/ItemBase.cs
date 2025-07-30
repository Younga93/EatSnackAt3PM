using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour, IInteractable
{
    public abstract void OnInteract();
}
