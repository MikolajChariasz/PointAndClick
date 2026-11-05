using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Inventory : MonoBehaviour
{
    public static Inventory I { get; private set; }

    readonly List<ItemData> items = new();
    public IReadOnlyList<ItemData> Items => items;
    public ItemData Selected { get; private set; }
    public event Action Changed;

    void Awake() => I = this;

    public bool Has(ItemData item) => items.Contains(item);

    public void Add(ItemData item)
    {
        if (item == null || items.Contains(item)) return;
        items.Add(item);
        Changed?.Invoke();
    }

    public void Remove(ItemData item)
    {
        if (!items.Remove(item)) return;
        if (Selected == item) Selected = null;
        Changed?.Invoke();
    }

    public void Select(ItemData item)
    {
        Selected = (Selected == item) ? null : item;   // ponowny klik odznacza
        Changed?.Invoke();
    }

    public void Load(IEnumerable<ItemData> list)
    {
        items.Clear();
        items.AddRange(list);
        Selected = null;
        Changed?.Invoke();
    }
}