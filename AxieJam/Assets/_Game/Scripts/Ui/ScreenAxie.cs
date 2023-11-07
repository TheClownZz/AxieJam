using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ScreenAxie : ScreenBase
{
    [SerializeField] Image imgBg;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] List<ItemAxie> itemList;

    public override void OnInit()
    {
        base.OnInit();
        for (int i = 0; i < (int)PlayerType.None; i++)
        {
            itemList[i].SetPlayerType((PlayerType)i);
        }
    }

    public override void OnShow()
    {
        base.OnShow();
        for (int i = 0; i < (int)PlayerType.None; i++)
        {
            itemList[i].UpdateUI();
        }
        scrollRect.DOVerticalNormalizedPos(1, 1f).SetSpeedBased(true);

    }

    public void SetBg(Sprite sprite)
    {
        imgBg.sprite = sprite;
    }
    public void OnBtnBackClick()
    {
        OnHide();
        UIManager.Instance.ShowScreen<ScreenHome>();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }
}
