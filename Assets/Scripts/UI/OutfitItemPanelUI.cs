using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutfitItemPanelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] Image itemImage;
    [SerializeField] Toggle equipToggle;

    private int id; //아이템 id

    private void Awake()
    {

    }
    public void Setting(int id)
    {
        this.id = id;

        //string imageFile = 
        //    string imageFile = GameManager.teamMembers[idx / numberOfImage] + "_" + (idx % numberOfImage + 1);
        //    frontImage.sprite = Resources.Load<Sprite>(imageFile);
        //    isBomb = false;
    }


}
