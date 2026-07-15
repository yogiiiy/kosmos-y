using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Kosmos Y/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int sellPrice;
}