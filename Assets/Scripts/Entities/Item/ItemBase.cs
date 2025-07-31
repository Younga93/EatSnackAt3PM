using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour, IInteractable, IAttractable
{
    [SerializeField] private ItemType itemType;

    public abstract void OnInteract(PlayerController player);

    public ItemType GetItemType() { return itemType; }

    public void AttractedBy(Vector3 position)
    {
        transform.position = Vector3.Lerp(transform.position, position, 0.1f);
    }
}
