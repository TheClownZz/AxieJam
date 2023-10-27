using System.Collections.Generic;
using UnityEngine;


public class ClearParticle : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> particleList;
    private void OnEnable()
    {
        foreach (ParticleSystem p in particleList)
        {
            p.Clear();
        }
    }
}
