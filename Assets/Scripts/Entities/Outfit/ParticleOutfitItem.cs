using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOutfitItem : OutfitItemBase
{
    public ParticleOutfitItem(int id, string name, string imageFileName) : base(id, name, imageFileName)
    {
    }
    public override void EquipOutfitItem(PlayerController player)
    {
        //To do: 장착 시 플레이어에게 적용할 것.
    }
}
