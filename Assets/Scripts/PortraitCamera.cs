using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PortraitCamera : MonoBehaviour
{
    [SerializeField] float targetWidth = 3f;
    [SerializeField] float targetHeight = 4f;

    Camera cam;
    int lastW, lastH;

    void Awake() => cam = GetComponent<Camera>();

    void Update()
    {
        if (Screen.width == lastW && Screen.height == lastH) return;
        lastW = Screen.width;
        lastH = Screen.height;

        float target = targetWidth / targetHeight;          // 0.75
        float screen = (float)Screen.width / Screen.height;

        if (screen > target)   // ekran szerszy ni¿ zdjêcie: paski po bokach
        {
            float w = target / screen;
            cam.rect = new Rect((1 - w) / 2f, 0, w, 1);
        }
        else                   // ekran wê¿szy: paski u góry i na dole
        {
            float h = screen / target;
            cam.rect = new Rect(0, (1 - h) / 2f, 1, h);
        }
    }
}