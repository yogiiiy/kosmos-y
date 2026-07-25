using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailPanel : MonoBehaviour
{
    public static ItemDetailPanel Instance;

    [SerializeField] private GameObject detailPanel;
    [SerializeField] private Image detailIcon;
    [SerializeField] private TextMeshProUGUI detailNameText;
    [SerializeField] private TextMeshProUGUI detailDescriptionText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDetail(ItemData itemData)
    {
        detailPanel.SetActive(true);
        detailIcon.sprite = itemData.icon;
        detailNameText.text = itemData.itemName;
        detailDescriptionText.text = itemData.description;
    }
}