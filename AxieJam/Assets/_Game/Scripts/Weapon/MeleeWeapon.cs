public class MeleeWeapon : Weapon
{
    //[SerializeField] protected Hitbox hitBox;
    //[SerializeField] protected Transform hitter;
    //[SerializeField] protected FxController particleController;
    //[SerializeField] float minFxTime = 0.5f;
    //bool isCheckHit;
    //float effectTime;


    //public override void OnInits(Character characterControl)
    //{
    //    base.OnInits(characterControl);
    //    hitBox.OnInits(this);
    //    effectTime = MathF.Min(minFxTime, coolDown);

    //}

   
    //public override void OnAttack()
    //{
    //    base.OnAttack();
    //    if (particleController)
    //        PlayeEffect();
    //    hitBox.SetActive(true);
    //}

    //private void FixedUpdate()
    //{
    //    if (hitBox.gameObject.activeInHierarchy)
    //        isCheckHit = true;
    //}

    //private void LateUpdate()
    //{
    //    if (isCheckHit)
    //    {
    //        isCheckHit = false;
    //        hitBox.SetActive(false);
    //    }
    //}

    //private void PlayeEffect()
    //{
    //    particleController.transform.position = hitter.position;
    //    particleController.transform.rotation = hitter.rotation;
    //    particleController.Play();
    //    DOVirtual.DelayedCall(effectTime, () =>
    //    {
    //        particleController.Stop();
    //    });
    //}

    //public override void OnClear()
    //{
    //    particleController.transform.SetParent(hitter.transform);
    //    base.OnClear();
    //}
}
