using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OutfitItemBase
{
    private int id;
    private string name;
    private string imageFileName;
    private bool isEquipped;

    public OutfitItemBase(int id, string name, string imageFileName, bool isEquipped = false)
    {
        this.id = id;
        this.name = name;
        this.imageFileName = imageFileName;
        this.isEquipped = isEquipped;
    }
    public abstract void EquipOutfitItem(PlayerController player);
}
