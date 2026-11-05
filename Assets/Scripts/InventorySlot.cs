using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] GameObject highlight;
    [SerializeField] Button button;

    public void Setup(ItemData item, bool selected)
    {
        icon.sprite = item.icon;
        highlight.SetActive(selected);
        button.onClick.AddListener(() => Inventory.I.Select(item));
    }
}