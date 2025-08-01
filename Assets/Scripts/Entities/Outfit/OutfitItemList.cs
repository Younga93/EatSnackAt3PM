using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutfitItemList : MonoBehaviour
{
    [SerializeField] GameObject itemPanel;

    // Start is called before the first frame update
    void Start()
    {
        float initialX = 360f;
        float initialY = 565f;
        int index = 0;
        foreach (OutfitItemBase item in OutfitItemData.allOutfitItems) //for문으로 할걸.
        {
            GameObject go = Instantiate(itemPanel, this.transform);

            float x = initialX + (400 * index++);
            float y = initialY;
            
            go.GetComponent<RectTransform>().position = new Vector2(x,y);
            //go.transform.position = new Vector2(x, 0);
            go.GetComponent<OutfitItemPanelUI>().Setting(item.Id);
        }

        //To do: 아이템 4개 이상일 경우, 스크롤하는 기능 추가
    }
}
