using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerOutfitController : MonoBehaviour
{
    SpriteRenderer[] hairRenderers;
    SpriteRenderer[] hairpinRenderers; //피스 하나뿐이긴 한데 통일감 위해 배열
    SpriteRenderer[] handbandRenderers;
    SpriteRenderer[] shoesRendererss;

    private void Awake()
    {
        hairRenderers = GetComponentsInChildren<SpriteRenderer>(true).Where(x => x.CompareTag("Hair")).ToArray();
        hairpinRenderers = GetComponentsInChildren<SpriteRenderer>(true).Where(x => x.CompareTag("HairPin")).ToArray();
        handbandRenderers = GetComponentsInChildren<SpriteRenderer>(true).Where(x => x.CompareTag("HandBand")).ToArray();
        shoesRendererss = GetComponentsInChildren<SpriteRenderer>(true).Where(x => x.CompareTag("Shoes")).ToArray();
        //ChangeColorByTag(Color.red, "Hair");
    }

    public void ApplyAllItemsEquipped()
    {
        int[] ids = OutfitItemData.GetEquippedOutfitItemIds();
        foreach (int id in ids)
        {
            OutfitItemBase item = OutfitItemData.GetOutfitItemFromUserItemsById(id);

            if (item != null && item is ColorOutfitItem colotItem)
            {
                ChangeColorByTag(colotItem.outfitColor, colotItem.partTag);
            }
        };
    }
    public void ChangeColorByTag(Color color, string tagName)
    {
        SpriteRenderer[] renderers;
        switch(tagName)
        {
            case "Hair":
                renderers = hairRenderers;
                break;
            case "HairPin":
                renderers = hairpinRenderers;
                break;
            case "HandBand":
                renderers = handbandRenderers;
                break;
            case "Shoes":
                renderers = shoesRendererss;
                break;
            default:
                renderers = new SpriteRenderer[0];  //그냥 빈 렌더러
                Debug.Log($"{tagName}과 일치하는 파트가 없습니다.");
                break;
        }
        ChangeColor(color, renderers);
    }
    public void ChangeColor(Color color, SpriteRenderer[] renderers)
    {
        foreach (var sr in renderers)
        {
            if (sr == null)
            {
                Debug.LogError("renderers 배열에 null이 포함되어 있음!");
                continue;
            }
            sr.color = color;
        }
    }
}
