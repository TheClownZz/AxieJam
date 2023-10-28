using DG.Tweening;
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
        tmpHp.SetText("{0}/{1}", current, max);
    }

    public void UpdateMana(float percen)
    {
        percen = Mathf.Clamp(percen, 0, 1);
        imgMana.fillAmount = percen;
        tmpMana.SetText(((int)(percen * 100)).ToString() + "%");
    }
}
