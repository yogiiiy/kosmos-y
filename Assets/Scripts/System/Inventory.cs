using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private List<InventoryItem> items = new List<InventoryItem>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(ItemData itemData, int quantity = 1)
    {
        InventoryItem existingItem = items.Find(i => i.itemData == itemData);

        if (existingItem != null)
        {
            existingItem.quantity += quantity;
        }
        else
        {
            items.Add(new InventoryItem(itemData, quantity));
        }

        Debug.Log($"{itemData.itemName} x{quantity} ditambahkan. Total: {GetQuantity(itemData)}");
    }

    public int GetQuantity(ItemData itemData)
    {
        InventoryItem item = items.Find(i => i.itemData == itemData);
        return item != null ? item.quantity : 0;
    }

    public List<InventoryItem> GetAllItems()
    {
        return items;
    }
}