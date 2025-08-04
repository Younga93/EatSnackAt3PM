using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutfitItemList : MonoBehaviour
{
    [SerializeField] GameObject itemPanel;
    [SerializeField] Transform content;

    // Start is called before the first frame update
    void Start()
    {
        foreach (OutfitItemBase item in OutfitItemData.allOutfitItems) //for문으로 할걸.
        {
            GameObject go = Instantiate(itemPanel, content);
            go.GetComponent<OutfitItemPanelUI>().Setting(item.Id);
        }
    }
}
