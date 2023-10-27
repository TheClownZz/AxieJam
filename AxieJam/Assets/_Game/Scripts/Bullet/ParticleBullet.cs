using System.Collections.Generic;
using UnityEngine;

public class ParticleBullet : Bullet
{
    [SerializeField] List<ParticleSystem> particleList;
    [SerializeField] List<ParticleSystem> trailparticleList;
    public void SetColor(Color color)
    {
        foreach (var particle in particleList)
        {
            var main = particle.main;
            main.startColor = color;
        }

        foreach (var particle in trailparticleList)
        {
            var col = particle.colorOverLifetime;

            col.enabled = true;

            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(color * 0.9f, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

            col.color = grad;
        }
    }

}
