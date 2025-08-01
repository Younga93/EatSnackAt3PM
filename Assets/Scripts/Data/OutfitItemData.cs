using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OutfitItemData  //게임 내 아웃핏 데이터
{
    public static readonly List<OutfitItemBase> outfitItems = new();

    public static void InitializeData()
    {
        outfitItems.Clear();
        outfitItems.Add(new ParticleOutfitItem(1, "Anger", "dummy"));
    }
}
