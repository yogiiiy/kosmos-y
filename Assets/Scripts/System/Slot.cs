using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;

    private InventoryItem currentItem;

    public void SetSlot(InventoryItem item)
    {
        if (item == null)
        {
            Clear();
            return;
        }

        currentItem = item;
        iconImage.gameObject.SetActive(true);
        iconImage.sprite = item.itemData.icon;
        quantityText.text = item.quantity > 1 ? item.quantity.ToString() : "";
    }

    public void Clear()
    {
        currentItem = null;
        iconImage.gameObject.SetActive(false);
        iconImage.sprite = null;
        quantityText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
{
    Debug.Log("Slot diklik. currentItem null? " + (currentItem == null));
    if (currentItem != null)
    {
        ItemDetailPanel.Instance.ShowDetail(currentItem.itemData);
    }
}
}