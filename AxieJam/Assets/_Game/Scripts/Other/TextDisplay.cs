using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextDisplay : MonoBehaviour
{
    const string MISS = "Miss";
    const string ONE_HIT = "Bingo";

    [SerializeField] Sprite crit;
    [SerializeField] Sprite oneHit;

    [SerializeField] TextMeshPro tmp;
    [SerializeField] SpriteRenderer render;

    public void ShowDamage(float damage, bool isCrit)
    {
        if (isCrit)
        {
            tmp.color = GameManager.Instance.gameConfig.critColor;
            tmp.transform.localScale = Vector3.one * 1.5f;
            render.sprite = crit;
            render.enabled = true;
        }
        else
        {
            tmp.color = Color.white;
            render.enabled = false;
            tmp.transform.localScale = Vector3.one;
        }

        tmp.SetText(((int)damage).ToString());
    }

    public void ShowMiss()
    {
        tmp.SetText(MISS);
        tmp.color = Color.white;
        render.enabled = false;
        tmp.transform.localScale = Vector3.one;
    }

    public void ShowOneHit()
    {
        tmp.SetText(ONE_HIT);
        tmp.color = Color.red;
        render.sprite = oneHit;
        render.enabled = true;
        tmp.transform.localScale = Vector3.one * 1.5f;
    }

}
