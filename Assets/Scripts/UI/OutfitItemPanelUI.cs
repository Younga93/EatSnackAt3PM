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
    [SerializeField] Button purchaseButton;

    private OutfitItemBase item;


    private void Awake()
    {
        equipToggle.onValueChanged.AddListener(OnEquipToggleChanged);
        purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
    }
    public void Setting(int id)
    {
        item = OutfitItemData.GetOutfitItemById(id);

        itemName.text = item.Name;
        itemImage.sprite = Resources.Load<Sprite>(item.ImageFileName);
        equipToggle.isOn = item.IsEquipped;

        purchaseButton.GetComponentInChildren<TextMeshProUGUI>(true).text = item.Price.ToString();
        if (OutfitItemData.GetUserOutfitItemById(item.Id) == null)
        {
            equipToggle.gameObject.SetActive(false);
            purchaseButton.gameObject.SetActive(true);
        }
        else
        {
            equipToggle.gameObject.SetActive(true);
            purchaseButton.gameObject.SetActive(false);
        }
    }
    void OnEquipToggleChanged(bool isOn)
    {
        item.EquipOutfitItem(isOn);

        Debug.Log($"{item.Name}이 장착되었나? {item.IsEquipped}");
    }
    void OnPurchaseButtonClicked()
    {

    }
}
