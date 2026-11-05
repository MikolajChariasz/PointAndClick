using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]   // musi powstaæ przed innymi skryptami
public class GameState : MonoBehaviour
{
    public static GameState I { get; private set; }

    readonly HashSet<string> flags = new();
    public event Action Changed;

    void Awake() => I = this;

    public bool Has(string flag) => flags.Contains(flag);
    public IEnumerable<string> All => flags;

    public void Set(string flag, bool value = true)
    {
        bool changed = value ? flags.Add(flag) : flags.Remove(flag);
        if (changed) Changed?.Invoke();
    }

    public void Load(IEnumerable<string> list)
    {
        flags.Clear();
        foreach (var f in list) flags.Add(f);
        Changed?.Invoke();
    }
}
