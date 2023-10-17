using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Setcursor : MonoBehaviour
{
    [SerializeField] Texture2D aimTexture;
    [SerializeField] Texture2D normalTexture;


    private void Start()
    {
        SetNormal();
    }
    public void SetAim()
    {
        Cursor.SetCursor(aimTexture, Vector2.zero, CursorMode.Auto);
    }

    public void SetNormal()
    {
        Cursor.SetCursor(normalTexture, Vector2.zero, CursorMode.Auto);
    }


}
