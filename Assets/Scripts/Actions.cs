using UnityEngine;

public class Actions : MonoBehaviour
{
    public void GoToView(GameObject view) => ViewManager.I.GoTo(view);
    public void Back() => ViewManager.I.Back();
    public void SetFlag(string flag) => GameState.I.Set(flag);
    public void ClearFlag(string flag) => GameState.I.Set(flag, false);
}
