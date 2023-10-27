using System.Collections;
using UnityEngine;

public class EnemyBuffSpeed : EnemyComponent
{
    [SerializeField] float coolDown = 3;
    [SerializeField] float duraction = 2;
    [SerializeField] float speedRate = 4.5f;


    float step;
    WaitForSeconds delayStep;
    WaitForSeconds delayDuration;

    Enemy eControl;
    private void Awake()
    {
        step = coolDown - duraction;
        delayStep = new WaitForSeconds(step);
        delayDuration = new WaitForSeconds(duraction);

    }
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        eControl = (Enemy)enemy;
        StartCoroutine(IActive());

    }



    public override void OnDead()
    {
        base.OnDead();
        StopAllCoroutines();
    }

    IEnumerator IActive()
    {
        while (true)
        {
            eControl.GetECom<EnemyMove>().UpdateSpeed(speedRate);
            yield return delayDuration;
            eControl.GetECom<EnemyMove>().UpdateSpeed(1);
            yield return delayStep;
        }
    }
}
