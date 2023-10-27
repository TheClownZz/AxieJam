using UnityEngine;

public class UiImageSizeHelper : MonoBehaviour
{
    const float defaultW = 16f;
    const float defaultH = 9f;
    private void Start()
    {
        float scale = (defaultH * Screen.width) / (defaultW * Screen.height);
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = rect.sizeDelta * scale;
    }
}
