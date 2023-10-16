using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSizeHelper : MonoBehaviour
{
    const float defaultW = 16f;
    const float defaultH = 9f;
    private void Start()
    {
        float scale = (defaultH * Screen.width) / (defaultW * Screen.height);
        transform.localScale = scale * Vector3.one;
    }
}
