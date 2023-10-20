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

    public void UpdateHealth(float percen, float time =0.05f)
    {
        imgHealth.DOFillAmount(percen, time);
    }

    public void UpdateMana(float percen)
    {
        imgMana.fillAmount = percen;
    }
}
