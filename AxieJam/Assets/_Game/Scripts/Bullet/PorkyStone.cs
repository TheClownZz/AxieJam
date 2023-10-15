using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PorkyStone : Bullet
{
    public float baseDamage;
    public bool isFollow = false;
    [SerializeField] float rotateSpeed = 100;
    float moveTime;
    float currentTime = 0;
    [SerializeField] float force = 100;
    [SerializeField] float radius = 0.3f;
    [SerializeField] Transform rockContent;
    Vector3 p0, p1, p2;

    [SerializeField] List<Rigidbody2D> partList;
    List<Vector3> cachedPosList = new List<Vector3>();
    List<Quaternion> rotateList = new List<Quaternion>();
    [SerializeField] Transform explosionPoint;

    Tween tween;
    bool isExplosion;
    void Awake()
    {
        for (int i = 0; i < partList.Count; i++)
        {
            cachedPosList.Add(partList[i].transform.localPosition);
            rotateList.Add(partList[i].transform.localRotation);
        }
    }

    void OnDisable()
    {
        if (tween != null)
            tween.Kill();
    }
    protected override void Update()
    {
        if (!isFollow)
            return;
        rockContent.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        currentTime += Time.deltaTime;
        float t = currentTime / moveTime;
        Vector3 pa = Vector3.Lerp(p0, p1, t);
        Vector3 pb = Vector3.Lerp(p1, p2, t);
        transform.position = Vector3.Lerp(pa, pb, t);
        if (t > 0.95f)
        {
            if (!isExplosion)
                Clear();
        }
    }

    public void Setup(float damage, Weapon wp, float time)
    {
        weapon = wp;
        isFollow = false;
        isExplosion = false;
        currentTime = 0;
        moveTime = time;
        baseDamage = damage;

        for (int i = 0; i < partList.Count; i++)
        {
            partList[i].transform.localPosition = cachedPosList[i];
            partList[i].transform.localRotation = rotateList[i];
            partList[i].bodyType = RigidbodyType2D.Kinematic;
            partList[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        }
        GetComponent<CircleCollider2D>().enabled = false;

    }

    public void Thrown(Vector3 pos)
    {
        isFollow = true;
        p2 = pos;
        p0 = transform.position;
        p0.z = 0;
        p1.z = 0;
        Vector2 dir = pos - transform.position;
        float x = transform.position.x + Random.Range(0.5f, 0.8f) * dir.x;
        float y = transform.position.y + Random.Range(0.5f, 0.8f) * Mathf.Abs(dir.y);
        p1 = new Vector3(x, y, transform.position.z);
        GetComponent<CircleCollider2D>().enabled = true;

    }

    public override void Clear()
    {
        isExplosion = true;
        isFollow = false;
        GetComponent<CircleCollider2D>().enabled = false;
        for (int i = 0; i < partList.Count; i++)
        {
            Vector2 dir = partList[i].position - (Vector2)explosionPoint.position;
            dir = dir * radius / dir.magnitude;
            partList[i].bodyType = RigidbodyType2D.Dynamic;
            partList[i].AddForce(dir * force, ForceMode2D.Impulse);
            partList[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = true;

        }

        if (hitClip && Time.time - timePlaySound > 0.1f)
        {
            timePlaySound = Time.time;
            AudioManager.Instance.PlaySound(hitClip);
        }

        tween = DOVirtual.DelayedCall(3, () =>
          {
              base.Clear();
          });
    }

    protected override void HitCharacter(Character character)
    {
        CharacterStat stat = weapon.GetCharacterStat();
        float damage = baseDamage;
        float critDamage = stat.critDamage;

        float critRate = stat.critRate;
        bool isCrit = Random.value <= critRate;
        if (isCrit)
        {
            damage += (damage * critDamage);
        }
        damage = Random.Range(0.9f * damage, 1.1f * damage);

        character.TakeDamage(damage, isCrit);

        AddEffect(character);
    }
}
