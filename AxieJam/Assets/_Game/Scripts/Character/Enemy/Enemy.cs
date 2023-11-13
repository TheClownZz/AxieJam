using UnityEngine;
using DG.Tweening;

public class Enemy : Character
{
    const float timeDelayDespawn = 1.5f;
    [SerializeField] float spawItemTime = 1f;
    [SerializeField] protected AudioClip deadClip;
    [SerializeField] Transform spawnFxPrefab;

    protected Transform spawnFx;
    protected Tween spawnTween;
    protected Tween clearTween;
    protected Tween itemTween;


    [HideInInspector] public WaveStat waveStat;
    [HideInInspector] public float currspawItemTime;

    public override void OnInit()
    {
        base.OnInit();
        SetState(CharacterState.Alive);
        currspawItemTime = spawItemTime;
    }
    public void DelaySpawn(float time, Vector3 pos)
    {
        spawnFx = PoolManager.Instance.SpawnObject(spawnFxPrefab);
        spawnFx.position = pos;
        gameObject.SetActive(false);
        spawnFx.transform.SetParent(GameManager.Instance.GetMapTf().GetChild(1));

        spawnTween = GameManager.Instance.DelayedCall(time, () =>
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

        clearTween = GameManager.Instance.DelayedCall(timeDelayDespawn, () =>
        {
            Clear();
            GameManager.Instance.levelController.RemoveEnemy(this);
        });

        itemTween = GameManager.Instance.DelayedCall(currspawItemTime, () =>
        {
            SpawnItem(deadClip);
        });
    }

    protected void SpawnItem(AudioClip clip)
    {
        if (clip)
        {
            AudioManager.Instance.PlaySound(clip);
        }
        float foodRandom = Random.value;
        EnemyAsset enemyAsset = GetComponent<SetupEnemyData>().asset;
        if (foodRandom < enemyAsset.data.foodDropRate)
        {
            PlayerType type = (PlayerType)Random.Range(0, (int)PlayerType.None);
            FoodConfig config = DataManager.Instance.GetAsset<FoodAsset>().GetConfig(type);

            Transform item = PoolManager.Instance.SpawnObject(PoolType.FoodItem);
            item.transform.position = transform.position + (Vector3)Random.insideUnitCircle * 0.5f;
            item.transform.SetParent(GameManager.Instance.GetMapTf().GetChild(1));
            item.GetComponent<FoodItem>().SetConfig(config);
        }
        else
        {
            float potionRandom = Random.value;
            if (potionRandom < enemyAsset.data.potionDropRate)
            {
                PlayerType type = (PlayerType)Random.Range(0, (int)PlayerType.None);
                PotionConfig config = DataManager.Instance.GetAsset<PotionAsset>().GetConfig(type);

                Transform item = PoolManager.Instance.SpawnObject(PoolType.PotionItem);
                item.transform.position = transform.position + (Vector3)Random.insideUnitCircle * 0.5f;
                item.transform.SetParent(GameManager.Instance.GetMapTf().GetChild(1));
                item.GetComponent<PotionItem>().SetConfig(config);

            }

        }

    }
    public void Clear()
    {
        if (clearTween != null)
            clearTween.Kill();
        if (itemTween != null)
            itemTween.Kill();
        foreach (var comp in componentList)
            comp.Clear();
        PoolManager.Instance.DespawnObject(transform);
    }

    public override void SetStat()
    {
        var data = GetComponent<SetupEnemyData>().asset.data;
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
        SetState(CharacterState.Hit);
        DisableEnemy(true);
        return GetECom<EnemyHp>().TakeDamage(damage, isCrit);
    }

    public void TakePosionDamage(float damage)
    {
        GetECom<EnemyHp>().TakeDamage(damage, false);
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
