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
    [SerializeField] TextMeshProUGUI tmpFood;
    [SerializeField] TextMeshProUGUI tmpPotion;
    [SerializeField] TextMeshProUGUI tmpSelect;
    [SerializeField] Image imgSelected;
    [SerializeField] Image imgIcon;
    [SerializeField] Image imgFood;
    [SerializeField] Image imgPotion;
    [SerializeField] Button btnSelect;
    private void Awake()
    {
        btnSelect.onClick.AddListener(OnBtnSelectClick);
    }
    public void UpdateUi()
    {
        var data = DataManager.Instance.GetData<DataUser>().GetDataPlayer(playerType);

        var listAsset = DataManager.Instance.GetAsset<PlayerListAsset>().GetAsset(playerType);

        var levelConfig = listAsset.data.GetLevelConfig(data.level - 1);
        var skillConfig = listAsset.data.GetSkillConfig(data.levelSkill - 1);

        tmpLevel.SetText("lv.{0}", data.level);
        tmpSkill.SetText("lv.{0}", data.levelSkill);


        tmpFood.SetText("{0}/{1}", data.itemLevelCount, levelConfig.item);
        tmpPotion.SetText("{0}/{1}", data.itemSkillCount, skillConfig.defaultValue.itemRequire);
        imgIcon.sprite = listAsset.data.avatar;

        imgFood.sprite = DataManager.Instance.GetAsset<FoodAsset>().GetConfig(playerType).sprite;
        imgPotion.sprite = DataManager.Instance.GetAsset<PotionAsset>().GetConfig(playerType).sprite;

    }

    void OnBtnSelectClick()
    {
        Debug.LogError("OnBtnSelectClick");
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
