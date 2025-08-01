using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOutfitItem : OutfitItemBase
{
    public readonly Color outfitColor;

    public ColorOutfitItem(int id, string name, int price, string imageFileName, Color color) : base(id, name, price, imageFileName)
    {
        outfitColor = color;
    }

    public override void ApplyOutfitItem(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}
