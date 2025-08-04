using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOutfitItem : OutfitItemBase
{
    public readonly Color outfitColor;
    public readonly string partTag;

    public ColorOutfitItem(int id, string name, int price, string imageFileName, Color color, string outfitTag) : base(id, name, price, imageFileName)
    {
        outfitColor = color;
        this.partTag = outfitTag;
    }

    public override void ApplyOutfitItem(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}
