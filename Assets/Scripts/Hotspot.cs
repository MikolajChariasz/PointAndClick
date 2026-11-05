using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Hotspot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Warunki (opcjonalne)")]
    public string requiredFlag;
    public ItemData requiredItem;
    public bool consumeItem = true;

    [Header("Efekty")]
    public UnityEvent onActivated;
    public UnityEvent onFailed;

    [Header("Kursor")]
    [SerializeField] Texture2D hoverCursor;

    public void OnPointerClick(PointerEventData e)
    {
        if (ViewManager.I.Busy) return;

        bool ok = true;
        if (!string.IsNullOrEmpty(requiredFlag) && !GameState.I.Has(requiredFlag)) ok = false;
        if (requiredItem != null && Inventory.I.Selected != requiredItem) ok = false;

        if (!ok) { onFailed.Invoke(); return; }

        if (requiredItem != null && consumeItem) Inventory.I.Remove(requiredItem);
        onActivated.Invoke();
    }

    public void OnPointerEnter(PointerEventData e)
    {
        if (hoverCursor)
            Cursor.SetCursor(hoverCursor, new Vector2(hoverCursor.width / 2f, hoverCursor.height / 2f), CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData e) => ResetCursor();
    void OnDisable() => ResetCursor();
    static void ResetCursor() => Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

    // Podgl¹d hotspotów w edytorze (zielony obrys)
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.8f);
        var poly = GetComponent<PolygonCollider2D>();
        if (poly)
        {
            for (int p = 0; p < poly.pathCount; p++)
            {
                var pts = poly.GetPath(p);
                for (int i = 0; i < pts.Length; i++)
                    Gizmos.DrawLine(transform.TransformPoint(pts[i]),
                                    transform.TransformPoint(pts[(i + 1) % pts.Length]));
            }
        }
        else
        {
            var c = GetComponent<Collider2D>();
            if (c) Gizmos.DrawWireCube(c.bounds.center, c.bounds.size);
        }
    }
}