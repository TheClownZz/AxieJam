using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAvt : MonoBehaviour
{
    [SerializeField] Image imgHealth;
    [SerializeField] Image imgMana;
    [SerializeField] Image imgIcon;
    [SerializeField] TextMeshProUGUI tmpHp;
    [SerializeField] TextMeshProUGUI tmpMana;
    public void SetAvt(Sprite avt)
    {
        imgIcon.sprite = avt;
    }

    public void UpdateHealth(int current, int max, float percen, float time = 0.2f)
    {
        imgHealth.DOKill();
        imgHealth.DOFillAmount(percen, time);
        var color = imgHealth.color;
        color.a = percen + 0.25f;
        imgHealth.color = color;
        tmpHp.SetText("{0}/{1}", current, max);
    }

    public void UpdateMana(float percen)
    {
        imgMana.fillAmount = percen;
        var color = imgMana.color;
        color.a = percen + 0.25f;
        imgMana.color = color;
        tmpMana.SetText(((int)(percen * 100)).ToString() + "%");
    }
}
