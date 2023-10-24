using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    public PlayerType playerType;

    [SerializeField] TextMeshProUGUI tmpLevel;
    [SerializeField] TextMeshProUGUI tmpSkill;
    [SerializeField] TextMeshProUGUI tmpSelect;
    [SerializeField] Image imgSelected;
    [SerializeField] Image imgIcon;
    [SerializeField] Button btnSelect;
    private void Awake()
    {
        btnSelect.onClick.AddListener(OnBtnSelectClick);
    }
    public void UpdateUi()
    {
        var data = DataManager.Instance.GetData<DataUser>().GetDataPlayer(playerType);

        var listAsset = DataManager.Instance.GetAsset<PlayerListAsset>().GetAsset(playerType);

        tmpLevel.SetText("lv.{0}", data.level);
        tmpSkill.SetText("lv.{0}", data.levelSkill);

        imgIcon.sprite = listAsset.data.avatar;

    }

    void OnBtnSelectClick()
    {
        SetSelect(true);
        UIManager.Instance.GetScreen<ScreenHome>().OnSelect(this);
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }

    public void SetSelect(bool value)
    {
        tmpSelect.SetText(value ? "Selected" : "Select");
        btnSelect.interactable = !value;
        imgSelected.enabled = value;
    }
}
