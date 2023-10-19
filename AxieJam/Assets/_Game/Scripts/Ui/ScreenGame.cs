using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScreenGame : ScreenBase
{
    [SerializeField] Button btnPlay;
    [SerializeField] TextMeshProUGUI tmpHp;
    [SerializeField] TextMeshProUGUI tmpCountDown;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip coolDownClip;
    public override void OnInit()
    {
        base.OnInit();
        btnPlay.onClick.AddListener(OnBtnPlayClick);
    }

    public override void OnShow()
    {
        base.OnShow();
    }

    public void SetHp(float hp)
    {
       // tmpHp.SetText(((int)hp).ToString());
    }

    public void UpdateCountDown(string text)
    {
        tmpCountDown.SetText(text);
    }


    
    public void OnBtnPlayClick()
    {
        btnPlay.gameObject.SetActive(false);
        GameManager.Instance.StartLevel();
        AudioManager.Instance.PlaySound(audioSource, coolDownClip);
        AudioManager.Instance.PlayOnceShot(AudioType.CoolDown);
    }
}
