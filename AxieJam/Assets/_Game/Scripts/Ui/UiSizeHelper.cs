using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSizeHelper : MonoBehaviour
{
    const float defaultW = 9f;
    const float defaultH = 16f;
    private void Start()
    {
        float scale = (defaultH * Screen.width) / (defaultW * Screen.height);
        transform.localScale = scale * Vector3.one;
    }
}
