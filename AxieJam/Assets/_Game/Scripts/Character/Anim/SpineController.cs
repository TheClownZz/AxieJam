using UnityEngine;
using Spine.Unity;

public class SpineController : MonoBehaviour
{
    [SerializeField] float runScale = 1.5f;
    [SerializeField] string Run = "run";
    [SerializeField] string Idle = "idle";
    [SerializeField] string Die = "die";
    [SerializeField] protected MeshRenderer meshRender;
    [SerializeField] protected SkeletonAnimation anim;

    protected Character control;

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        anim = GetComponentInChildren<SkeletonAnimation>();
        meshRender = anim.GetComponent<MeshRenderer>();
    }
#endif

    public virtual void OnInits(Character control)
    {
        this.control = control;
        SetTimeScale(1);

    }

    public virtual void OnDead()
    {

    }
    public void SetAnim(string anim)
    {

    }
    public virtual void SetAnim(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Alive:
            case CharacterState.Idle:
                anim.state.SetAnimation(0, Idle, true);
                SetTimeScale(1);
                break;
            case CharacterState.Die:
                OnDead();
                anim.state.SetAnimation(0, Die, false);
                SetTimeScale(1);

                break;
            case CharacterState.Run:
                anim.state.SetAnimation(0, Run, true);
                SetTimeScale(runScale);
                break;
            default:
                break;
        }
    }

    public void FlipX(float flip)
    {
        anim.skeleton.ScaleX = flip;
    }



    public void ShowRender(bool isShow)
    {
        meshRender.enabled = isShow;
    }
    protected void SetTimeScale(float timeScale)
    {
        anim.timeScale = timeScale;
    }

    public SkeletonDataAsset GetAsset()
    {
        return anim.skeletonDataAsset;
    }
}
