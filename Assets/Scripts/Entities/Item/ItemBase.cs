using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemType itemType;

    public abstract void OnInteract();

    public ItemType GetItemType() { return itemType; }

}
