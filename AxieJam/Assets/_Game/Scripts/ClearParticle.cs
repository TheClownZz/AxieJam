using System.Collections.Generic;
using UnityEngine;


public class ClearParticle : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> particleList;
    public void Clear()
    {
        foreach (ParticleSystem p in particleList)
        {
            p.Clear();
        }
    }
}
