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

        RetrieveOutfitInPlayeyPrefs();
    }

    public static OutfitItemBase GetOutfitItemFromAllItemsById(int id)
    {
        OutfitItemBase item = allOutfitItems.FirstOrDefault(x => x.Id == id);
        if (item == null) {
            Debug.LogWarning($"아이템 ID {id}는 존재하지 않습니다.");
        }
        return item;
    }
    public static OutfitItemBase? GetOutfitItemFromUserItemsById(int id)
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
        if(GetOutfitItemFromUserItemsById(id) != null)
        {
            Debug.LogWarning($"아이템 ID {id}는 유저가 이미 소지하고 있습니다.");
            return;
        }
        userOutfitItems.Add(GetOutfitItemFromAllItemsById(id));
    }
    public static int[] GetEquippedOutfitItemIds()
    {
        return userOutfitItems.Where(x => x.IsEquipped).Select(x => x.Id).ToArray();
    }
    public static int[] GetOwnedOutfitItemIds()
    {
        return userOutfitItems.Select(x => x.Id).ToArray();
    }
    public static void SaveOutfitInPlayeyPrefs()
    {
        int[] ownedIds = GetOwnedOutfitItemIds();
        string joinedOwnedIds = string.Join(",", ownedIds);
        PlayerPrefs.SetString("OutfitOwned", joinedOwnedIds);

        int[] equippedIds = GetEquippedOutfitItemIds();
        string joinedEquippedIds = string.Join(",", equippedIds);
        PlayerPrefs.SetString("OutfitEquipped", joinedEquippedIds);
    }
    public static void RetrieveOutfitInPlayeyPrefs()
    {
        //소지 아이템 아이디
        string savedOwned = PlayerPrefs.GetString("OutfitOwned", "");
        if (string.IsNullOrEmpty(savedOwned))
            return;
        int[] OwnedIds = savedOwned.Split(',')
            .Select(s => int.TryParse(s, out var id) ? id : -1)
            .Where(id => id >= 0)
            .ToArray();

        //착용 아이템 아이디
        string savedEquipped = PlayerPrefs.GetString("OutfitEquipped", "");
        if (string.IsNullOrEmpty(savedOwned))
            return;
        int[] EquippedIds = savedEquipped.Split(',')
            .Select(s => int.TryParse(s, out var id) ? id : -1)
            .Where(id => id >= 0)
            .ToArray();

        userOutfitItems.Clear();
        foreach (int id in OwnedIds)
        {
            OutfitItemBase item = GetOutfitItemFromAllItemsById(id);

            if (EquippedIds.Contains(id)) item.EquipOutfitItem(true);

            userOutfitItems.Add(item);
        }
    }
}
