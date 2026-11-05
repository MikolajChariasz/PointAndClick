using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class ViewManager : MonoBehaviour
{
    public static ViewManager I { get; private set; }

    [SerializeField] GameObject startView;
    [SerializeField] List<GameObject> allViews;   // WSZYSTKIE widoki
    [SerializeField] CanvasGroup fade;
    [SerializeField] GameObject backButton;
    [SerializeField] float fadeTime = 0.25f;

    readonly Stack<GameObject> history = new();
    GameObject current;

    public bool Busy { get; private set; }
    public event Action ViewChanged;

    void Awake() => I = this;

    void Start()
    {
        foreach (var v in allViews) v.SetActive(v == startView);
        current = startView;
        UpdateBack();
    }

    public void GoTo(GameObject view)
    {
        if (!Busy && view != current) StartCoroutine(Switch(view, true));
    }

    public void Back()
    {
        if (!Busy && history.Count > 0) StartCoroutine(Switch(history.Pop(), false));
    }

    IEnumerator Switch(GameObject next, bool push)
    {
        Busy = true;
        yield return Fade(0, 1);
        if (push) history.Push(current);
        current.SetActive(false);
        next.SetActive(true);
        current = next;
        UpdateBack();
        yield return Fade(1, 0);
        Busy = false;
        ViewChanged?.Invoke();
    }

    IEnumerator Fade(float from, float to)
    {
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            fade.alpha = Mathf.Lerp(from, to, t / fadeTime);
            yield return null;
        }
        fade.alpha = to;
    }

    void UpdateBack()
    {
        if (backButton) backButton.SetActive(history.Count > 0);
    }
}