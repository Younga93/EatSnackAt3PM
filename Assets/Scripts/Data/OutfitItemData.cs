using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class OutfitItemData  //게임 내 아웃핏 데이터
{
    public static readonly List<OutfitItemBase> allOutfitItems = new();
    public static List<OutfitItemBase> userOutfitItems = new();

    public static void InitializeData()
    {
        allOutfitItems.Clear();
        allOutfitItems.Add(new ColorOutfitItem(1, "Red Hair", 1000, "RedHair", Color.red, "Hair"));
        allOutfitItems.Add(new ColorOutfitItem(2, "Hair Pin", 2000, "BlueHairPin", Color.blue, "HairPin"));
        allOutfitItems.Add(new ColorOutfitItem(3, "HandBand", 3000, "GreenHairBand", Color.green, "HandBand"));
        allOutfitItems.Add(new ColorOutfitItem(4, "Shoes", 4000, "RedShoes", Color.magenta, "Shoes"));

        userOutfitItems.Add(allOutfitItems[0]);
    }

    public static OutfitItemBase GetOutfitItemById(int id)
    {
        OutfitItemBase item = allOutfitItems.FirstOrDefault(x => x.Id == id);
        if (item == null) {
            Debug.LogWarning($"아이템 ID {id}는 존재하지 않습니다.");
        }
        return item;
    }
    public static OutfitItemBase? GetUserOutfitItemById(int id)
    {
        OutfitItemBase? item = userOutfitItems.FirstOrDefault(x => x.Id == id); 
        if (item == null)
        {
            Debug.LogWarning($"아이템 ID {id}는 유저가 소지하고 있지 않습니다.");
        }
        return item;
    }
    public static void AddUserItemById(int id)
    {
        if(GetUserOutfitItemById(id) != null)
        {
            Debug.LogWarning($"아이템 ID {id}는 유저가 이미 소지하고 있습니다.");
            return;
        }
        userOutfitItems.Add(GetOutfitItemById(id));
    }
    public static List<OutfitItemBase>? GetEquippedOutfitItems()
    {
        List<OutfitItemBase> equippedOutfitItems;
        equippedOutfitItems = userOutfitItems.Where(x => x.IsEquipped == true).ToList();
        return equippedOutfitItems;
    }
}
