using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Enemy : Character
{
    static float timePlayHit;
    const float delayPlayHit = 0.1f;
    const float timeDelayDespawn = 1;
    [SerializeField] EnemyAsset asset;
    [HideInInspector] public WaveStat waveStat;

    Transform spawnFx;
    Tween spawnTween;
    Tween clearTween;
    public override void OnInit()
    {
        base.OnInit();
        SetState(CharacterState.Alive);
    }
    public void DelaySpawn(float time, Vector3 pos)
    {
        spawnFx = PoolManager.Instance.SpawnObject(PoolType.SpawnFx);
        spawnFx.position = pos;
        gameObject.SetActive(false);
        spawnTween = DOVirtual.DelayedCall(time, () =>
        {
            PoolManager.Instance.DespawnObject(spawnFx);
            spawnFx = null;
            gameObject.SetActive(true);

            OnInit();
            transform.position = pos;
        });
    }

    private void ClearDelaySpawn()
    {
        if (spawnTween != null)
            spawnTween.Kill();
        if (spawnFx)
        {
            PoolManager.Instance.DespawnObject(spawnFx);
            spawnFx = null;
        }
    }
    public T GetECom<T>() where T : EnemyComponent
    {
        foreach (var comp in componentList)
            if (comp is T)
                return comp as T;
        return null;
    }


    public void OnUpdate(float dt)
    {
        if (isDead || !gameObject.activeInHierarchy)
            return;
        foreach (var comp in componentList)
            comp.OnUpdate(dt);
    }

    public override void OnDead()
    {
        base.OnDead();
        foreach (var comp in componentList)
            comp.OnDead();
        if (Time.time - timePlayHit > delayPlayHit)
        {
            timePlayHit = Time.time;
            AudioManager.Instance.PlayOnceShot(AudioType.EnemyDead);
        }

        clearTween = DOVirtual.DelayedCall(timeDelayDespawn, () =>
        {
            Clear();
            GameManager.Instance.levelController.RemoveEnemy(this);
        });
    }

    public void Clear()
    {
        if (clearTween != null)
            clearTween.Kill();
        foreach (var comp in componentList)
            comp.Clear();
        PoolManager.Instance.DespawnObject(transform);
    }

    public override void SetStat()
    {
        var data = asset.data;
        stat.SetHp(data.hp)
            .Setarmor(data.armor)
            .SetDamage(data.damage)
            .SetAttackSpeed(data.attackSpeed)
            .SetMoveSpeed(data.moveSpeed);
    }
    public void SetWaveStat(WaveStat waveStat)
    {
        this.waveStat = waveStat;
        stat.SetHp(stat.hp * waveStat.hpRate).SetDamage(stat.damage * waveStat.damageRate);
    }

    public override float TakeDamage(float damage, bool isCrit)
    {
        return GetECom<EnemyHp>().TakeDamage(damage, isCrit);
    }
    public override void OnLose()
    {
        base.OnLose();
        ClearDelaySpawn();

    }
    public override void KnockBack(Vector2 dir, float force)
    {
        base.KnockBack(dir, force);
        GetCom<EnemyMove>().SetForceDir(dir, force);
    }

    public override void OnHitDone()
    {
        base.OnHitDone();
        DisableEnemy(false);
        SetState(CharacterState.Idle);
    }
}
