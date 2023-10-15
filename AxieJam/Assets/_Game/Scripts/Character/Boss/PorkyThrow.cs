using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class PorkyThrow : BossAttack
{
    const string takeEvent = "eTake";
    const string thorwEvent = "eThrow";
    [SerializeField] Transform hand;
    [SerializeField] Transform stonePf;
    [SerializeField] GameObject objAim;
    [SerializeField] Weapon weapon;
    [SerializeField] float moveTime = 2;

    [SerializeField] float normalDistance = 0.5f;
    PorkyStone currentStone;
    public override void OnInits(Character control)
    {
        base.OnInits(control);
        eControl.GetCom<BossMove>().SetAllowMove(true);
    }
    protected override void OnAttack()
    {
        base.OnAttack();
        eControl.GetCom<BossMove>().SetAllowMove(false);
        anim.state.SetAnimation(0, animName, false);
    }

    public override void OnDead()
    {
        base.OnDead();
        ClearBullet();
    }

    public override void OnWin()
    {
        base.OnWin();
        ClearBullet();
    }
    protected override void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.HandleEvent(trackEntry, e);
        if (e.Data.Name == takeEvent)
        {
            SpawnRock();
        }
        else if (e.Data.Name == thorwEvent)
        {
            ThrownRock();
        }
    }

    private void SpawnRock()
    {
        currentStone = PoolManager.Instance.SpawnObject(stonePf).GetComponent<PorkyStone>();
        currentStone.transform.position = hand.position;
        currentStone.transform.SetParent(hand);
        float distance = Vector3.Distance(hand.position, target.transform.position);
        distance = Mathf.Max(distance, 0.1f);
        float rate = distance / normalDistance;
        rate = Mathf.Min(1, rate);
        currentStone.Setup(damage, weapon, moveTime * rate);

        objAim.SetActive(true);
        objAim.transform.position = target.transform.position;
    }

    private void ThrownRock()
    {
        objAim.SetActive(false);
        currentStone.isFollow = true;
        currentStone.transform.SetParent(null);
        currentStone.Thrown(objAim.transform.position);
        currentStone = null;
    }

    protected override void OnEndAnim(TrackEntry trackEntry)
    {
        base.OnEndAnim(trackEntry);
        if (trackEntry.Animation.Name == animName)
        {
            isAttack = false;
            eControl.GetCom<BossMove>().SetAllowMove(true);
            eControl.SetState(CharacterState.Run);
        }
    }

    protected override void OnCompleteAnim(TrackEntry trackEntry)
    {
        base.OnCompleteAnim(trackEntry);
        if (trackEntry.Animation.Name == animName)
        {
            isAttack = false;
            eControl.GetCom<BossMove>().SetAllowMove(true);
            eControl.SetState(CharacterState.Run);
        }
    }

    protected override bool CheckRange()
    {
        if (target.isDead)
            return false;
        return Vector2.Distance(hand.transform.position, target.transform.position) < range;
    }

    public override void Clear()
    {
        base.Clear();
        ClearBullet();
    }

    private void ClearBullet()
    {
        if (currentStone)
        {
            PoolManager.Instance.DespawnObject(currentStone.transform);
            currentStone = null;
        }
    }
}
