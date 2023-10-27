using System.Collections.Generic;
using UnityEngine;

public class SlashParticle : FxController
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] List<ParticleSystem> particleSystemList;
    List<float> sizeList = new List<float>();

    private void Awake()
    {
        foreach (var partical in particleSystemList)
        {
            var main = partical.main;
            sizeList.Add(main.startSize.constant);
        }
    }
    public override void SetRange(float range)
    {
        base.SetRange(range);
        for (int i = 0; i < particleSystemList.Count; i++)
        {
            var main = particleSystemList[i].main;
            main.startSize = new ParticleSystem.MinMaxCurve(sizeList[i] * range, sizeList[i] * range);
        }
    }

    public override void Play()
    {
        particle.Play();
    }

    public override void Stop()
    {
        particle.Stop();
    }
}
