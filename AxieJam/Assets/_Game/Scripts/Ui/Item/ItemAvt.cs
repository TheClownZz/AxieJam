using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAvt : MonoBehaviour
{
    [SerializeField] Image imgHealth;
    [SerializeField] Image imgMana;
    [SerializeField] Image imgIcon;

    public void SetAvt(Sprite avt)
    {
        imgIcon.sprite = avt;
    }

    public void UpdateHealth(float percen, float time = 0.2f)
    {
        imgHealth.DOKill();
        imgHealth.DOFillAmount(percen, time);
        var color = imgHealth.color;
        color.a = percen + 0.25f;
        imgHealth.color = color;
    }

    public void UpdateMana(float percen)
    {
        imgMana.fillAmount = percen;
        var color = imgMana.color;
        color.a = percen + 0.25f;
        imgMana.color = color;
    }
}
