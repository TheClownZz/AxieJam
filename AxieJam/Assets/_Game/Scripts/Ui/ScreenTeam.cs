using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTeam : ScreenBase
{
    [SerializeField] List<ItemSelect> itemList;
    [HideInInspector] public List<ItemSelect> itemSelectedList = new List<ItemSelect>();
    [SerializeField] TeamAvtController teamAvtController;
    List<PlayerType> currentTeam = new List<PlayerType>();
    public override void OnShow()
    {
        base.OnShow();

        List<PlayerType> team = DataManager.Instance.GetData<DataUser>().GetTeam();
        currentTeam.AddRange(team);
        teamAvtController.UpdateTeam(team);
    }
    public void OnBtnBackClick()
    {
        OnHide();
        UIManager.Instance.ShowScreen<ScreenHome>();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }
}
