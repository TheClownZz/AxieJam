using System.Collections.Generic;
using UnityEngine;


public class ScreenAxie : ScreenBase
{
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
    }
    public void OnBtnBackClick()
    {
        OnHide();
        UIManager.Instance.ShowScreen<ScreenHome>();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }
}
