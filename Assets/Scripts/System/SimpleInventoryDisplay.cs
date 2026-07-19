using UnityEngine;
using TMPro;

public class SimpleInventoryDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;

    private void Update()
    {
        string result = "";
        foreach (var item in Inventory.Instance.GetAllItems())
        {
            result += item.itemData.itemName + " x" + item.quantity + "\n";
        }
        displayText.text = result;
    }
}