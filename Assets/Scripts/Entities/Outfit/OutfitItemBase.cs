using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OutfitItemBase
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }
    public string ImageFileName { get; private set; }
    public bool IsEquipped { get; private set; }

    public OutfitItemBase(int id, string name, int price, string imageFileName, bool isEquipped = false)
    {
        this.Id = id;
        this.Name = name;
        this.Price = price;
        this.ImageFileName = imageFileName;
        this.IsEquipped = isEquipped;
    }
    public void EquipOutfitItem(bool isEquipped)
    {
        this.IsEquipped = isEquipped;
    }
    public abstract void ApplyOutfitItem(PlayerController player);
}
