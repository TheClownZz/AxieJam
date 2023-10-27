using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyJump : EnemyMove
{
    const float jumpPower = 2;
    const float jumpTime = 0.3f;
    const float jumpDistance = 1.5f;
    const float jumnpCooldown = 1.5f;

    bool isJump;
    float activeTime;

    Coroutine coroutine;
    public override void OnInits(Character e)
    {
        base.OnInits(e);
        isJump = false;
        activeTime = Time.time;
        control.spineController.ShowRender(true);
    }
    protected override void UpdatePostion(float dt)
    {
        if (Time.time - activeTime > jumnpCooldown && direction != Vector3.zero)
        {
            activeTime = Time.time;
            coroutine = StartCoroutine(IJump(direction));
        }
    }

    public override void SetForceDir(Vector3 forceDir, float forceStr)
    {
        base.SetForceDir(forceDir, forceStr);
        CancleJump();
    }


    private void CancleJump()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        control.spineController.ShowRender(true);
        transform.DOKill();
    }
    IEnumerator IJump(Vector3 dir)
    {
        float time = 0;
        float maxTime = 0.2f;
        float flashTime = 0.1f;

        bool isShow = true;
        while (time < maxTime)
        {
            isShow = !isShow;
            control.spineController.ShowRender(isShow);
            yield return new WaitForSeconds(flashTime);
            if (control.isDisable)
            {
                CancleJump();
            }
            time += flashTime;
        }

        control.spineController.ShowRender(true);
        yield return new WaitForSeconds(flashTime);
        control.SetState(CharacterState.Run);

        Vector3 pos = transform.position + dir * jumpDistance;
        transform.DOJump(pos, jumpPower, 1, jumpTime)
        .OnUpdate(() =>
        {
            if (control.isDisable)
            {
                CancleJump();
            }
        })
        .OnComplete(() =>
        {
            control.SetState(CharacterState.Idle);
        });
    }

    protected override void UpdateState()
    {
        if (!isJump)
            control.SetState(CharacterState.Idle);
    }
}
