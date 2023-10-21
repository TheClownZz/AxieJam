using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Setcursor : MonoBehaviour
{
    [SerializeField] Texture2D aimTexture;
    [SerializeField] Texture2D normalTexture;

    bool isNormal = false;

    private void Start()
    {
        SetNormal();
    }
    public void SetAim()
    {
        if (!isNormal) return;
        isNormal = false;
        Cursor.SetCursor(aimTexture, Vector2.zero, CursorMode.Auto);
    }

    public void SetNormal()
    {
        if (isNormal) return;
        isNormal = true;
        Cursor.SetCursor(normalTexture, Vector2.zero, CursorMode.Auto);
    }


}
