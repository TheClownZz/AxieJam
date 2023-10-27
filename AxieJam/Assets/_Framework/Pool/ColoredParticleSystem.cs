using System.Collections.Generic;
using UnityEngine;

public class ColoredParticleSystem : PoolElement
{

    #region CONST
    #endregion

    #region EDITOR PARAMS

    public List<ParticleSystem> splash_PSList = new List<ParticleSystem>();

    #endregion

    #region PARAMS
    #endregion

    #region PROPERTIES
    #endregion

    #region EVENTS
    #endregion

    #region METHODS

    public void SetColor(Color inputColor)
    {
        foreach (var item in splash_PSList)
        {
            ParticleSystem.MainModule settings = item.main;
            settings.startColor = new ParticleSystem.MinMaxGradient(inputColor);
        }
    }

    #endregion

}
