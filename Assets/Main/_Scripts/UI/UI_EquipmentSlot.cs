using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemText;

    protected UI ui;
    public InventoryItem item;
    public EquipmentType slotType;

    private void OnValidate()
    {
        gameObject.name = "Equipment slot - " + slotType.ToString();
    }
    protected virtual void Start()
    {
        ui = GetComponentInParent<UI>();

    }
    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;
        
        itemImage.color = Color.white;
        if (item != null)
        {
            itemImage.sprite = item.data.itemIcon;

            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }
    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (item == null)
            return;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
            return;
        ui.itemToolTip.ShowToolTip(item.data as ItemData_Equipment);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (item == null)
            return;
        ui.itemToolTip.HideToolTip();
    }
}
