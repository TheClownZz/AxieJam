using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ScreenBase : MonoBehaviour
{
    #region CONST
    #endregion

    #region EDITOR PARAMS
    [SerializeField]
    public CanvasGroup cvGroup;
    public Animator uiAnimator;
    #endregion

    #region PARAMS
    public bool isShowing;
    #endregion

    #region PROPERTIES
    #endregion

    #region EVENTS
    #endregion

    #region METHODS
    public virtual void OnInit()
    {
        OnHide();
    }


    public virtual void OnShow()
    {
        cvGroup.SetActive(true);
        this.isShowing = true;

    }

    public virtual void OnHide()
    {
        cvGroup.SetActive(false);
        this.isShowing = false;
    }

    public virtual void OnRelease()
    {

    }

    public virtual bool OnBack()
    {
        return false;
    }

    public virtual string GetName()
    {
        return this.GetType().FullName;
    }

#if UNITY_EDITOR
#endif
    #endregion

    #region DEBUG
    #endregion
}
