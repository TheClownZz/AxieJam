using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiImageSizeHelper : MonoBehaviour
{
    const float defaultW = 9f;
    const float defaultH = 16f;
    private void Start()
    {
        float scale = (defaultH * Screen.width) / (defaultW * Screen.height);
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = rect.sizeDelta * scale;
    }
}
