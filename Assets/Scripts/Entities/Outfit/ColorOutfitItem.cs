using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOutfitItem : OutfitItemBase
{


    public ColorOutfitItem(int id, string name, string imageFileName) : base(id, name, imageFileName)
    {
    }
    public override void EquipOutfitItem(PlayerController player)
    {
        //To do: 장착 시 플레이어에게 적용할 것.
    }
}
