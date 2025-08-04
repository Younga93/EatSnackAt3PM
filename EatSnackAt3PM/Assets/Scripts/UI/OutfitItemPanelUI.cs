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
        OutfitItemData.RetrieveOutfitInPlayeyPrefs();
        item = OutfitItemData.GetOutfitItemFromAllItemsById(id);

        itemName.text = item.Name;
        itemImage.sprite = Resources.Load<Sprite>(item.ImageFileName);
        equipToggle.isOn = item.IsEquipped;

        purchaseButton.GetComponentInChildren<TextMeshProUGUI>(true).text = item.Price.ToString();
        if (OutfitItemData.GetOutfitItemFromUserItemsById(item.Id) == null)  //안갖고 있음.
        {
            SwitchToggleAndPanel(false);
        }
        else
        {
            SwitchToggleAndPanel(true);
        }
    }
    void OnEquipToggleChanged(bool isOn)
    {
        item.EquipOutfitItem(isOn);
        
        OutfitItemData.SaveOutfitInPlayeyPrefs();

        Debug.Log($"{item.Name}이 장착되었냐?: {item.IsEquipped}");
    }
    void SwitchToggleAndPanel(bool isOwned)
    {
        equipToggle.gameObject.SetActive(isOwned);
        purchaseButton.gameObject.SetActive(!isOwned);
    }
    void OnPurchaseButtonClicked()
    {
        bool isSuccessful = GameManager.Instance.TryPurchaseOutfitItemById(item.Id);
        if (isSuccessful)
        {
            SwitchToggleAndPanel(true);
            OutfitItemData.SaveOutfitInPlayeyPrefs();
            UIManager.Instance.UpdateStoreUI();
        }
    }
}
