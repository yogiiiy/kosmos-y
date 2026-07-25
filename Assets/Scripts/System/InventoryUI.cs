using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject detailPanel;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private int totalSlots = 24;

    private Slot[] slotUIs;
    private PlayerControls controls;
    private bool isOpen = false;

    private void Awake()
    {
        controls = new PlayerControls();
        GenerateSlots();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.ToggleInventory.performed += OnToggle;
    }

    private void OnDisable()
    {
        controls.Player.ToggleInventory.performed -= OnToggle;
        controls.Player.Disable();
    }

    private void GenerateSlots()
    {
        slotUIs = new Slot[totalSlots];

        for (int i = 0; i < totalSlots; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotParent);
            slotUIs[i] = slotObj.GetComponent<Slot>();
            slotUIs[i].Clear();
        }
    }

    private void OnToggle(InputAction.CallbackContext context)
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
        detailPanel.SetActive(false);

        if (isOpen)
            RefreshUI();
    }

    private void RefreshUI()
    {
        var items = Inventory.Instance.GetAllItems();

        for (int i = 0; i < slotUIs.Length; i++)
        {
            if (i < items.Count)
                slotUIs[i].SetSlot(items[i]);
            else
                slotUIs[i].Clear();
        }
    }
}