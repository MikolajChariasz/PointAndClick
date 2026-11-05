using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] InventorySlot slotPrefab;

    void OnEnable()
    {
        Inventory.I.Changed += Rebuild;
        Rebuild();
    }

    void OnDisable()
    {
        if (Inventory.I != null) Inventory.I.Changed -= Rebuild;
    }

    void Rebuild()
    {
        foreach (Transform c in container) Destroy(c.gameObject);
        foreach (var item in Inventory.I.Items)
            Instantiate(slotPrefab, container).Setup(item, item == Inventory.I.Selected);
    }
}