using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TabToggle : MonoBehaviour
{
    #region CONST
    #endregion

    #region EDITOR PARAMS
    [SerializeField]
    protected Toggle togTab;
    [SerializeField]
    protected TabBase tab;
    #endregion

    #region PARAMS    
    #endregion

    #region PROPERTIES
    #endregion

    #region EVENTS
    #endregion

    #region METHODS
    public virtual void OnInit()
    {
        togTab.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                OnShowToggleTab();
            }
            else
            {
                OnHideToggleTab();
            }
        });
    }

    public virtual void OnShowToggleTab()
    {
        tab.OnShowTab();
    }

    public virtual void OnHideToggleTab()
    {
        tab.OnHideTab();
    }

    public virtual void SetTab(TabBase tab)
    {
        this.tab = tab;
    }

    public virtual void SetToggle(bool isOn)
    {
        togTab.isOn = isOn;
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        if (togTab == null)
        {
            togTab = GetComponent<Toggle>();
            if (togTab == null)
            {
                togTab = gameObject.AddComponent<Toggle>();
            }
        }
    }
#endif
    #endregion

    #region DEBUG
    #endregion
}
