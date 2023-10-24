using UnityEngine;
using Spine.Unity;
using Unity.VisualScripting.FullSerializer;

public class SpineController : MonoBehaviour
{
    [SerializeField] protected float runScale = 1.5f;
    [SerializeField] protected string Run = "run";
    [SerializeField] protected string Idle = "idle";
    [SerializeField] protected string Die = "die";
    [SerializeField] protected MeshRenderer meshRender;
    [SerializeField] protected SkeletonAnimation anim;

    protected Character control;
    protected string dieAnim;

    protected GameConfig gameConfig;
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
        dieAnim = Die;
        gameConfig = GameManager.Instance.gameConfig;
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
                SetTimeScale(gameConfig.normalAnimSacle);
                break;
            case CharacterState.Die:
                OnDead();
                anim.state.SetAnimation(0, dieAnim, false);
                SetTimeScale(gameConfig.deadAnimScale);

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

    public void SetDieAnim(string anim)
    {
        dieAnim = anim;
    }
    float cachedTimeScale;
    public void Pause()
    {
        cachedTimeScale = anim.timeScale;
        anim.timeScale = 0;
    }

    public void Resume()
    {
        anim.timeScale = cachedTimeScale;
    }
}
