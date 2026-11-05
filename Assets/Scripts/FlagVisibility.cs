using UnityEngine;

public class FlagVisibility : MonoBehaviour
{
    public string flag;
    public bool visibleWhenSet = true;

    void OnEnable()
    {
        GameState.I.Changed += Refresh;
        Refresh();
    }

    void OnDisable()
    {
        if (GameState.I != null) GameState.I.Changed -= Refresh;
    }

    void Refresh()
    {
        bool show = GameState.I.Has(flag) == visibleWhenSet;
        foreach (Transform child in transform)
            child.gameObject.SetActive(show);
    }
}