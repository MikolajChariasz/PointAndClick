using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string displayName;
    public Sprite icon;
    [TextArea] public string description;
}