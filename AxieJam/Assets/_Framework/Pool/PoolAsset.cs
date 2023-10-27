using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolData
{
    public PoolType poolType;
    public Transform prefab;
}

[CreateAssetMenu]
public class PoolAsset : ScriptableObject
{

    #region CONST
    #endregion

    #region EDITOR PARAMS

    public List<PoolData> poolDataList = new List<PoolData>();

    #endregion

    #region PARAMS
    #endregion

    #region PROPERTIES
    #endregion

    #region EVENTS
    #endregion

    #region METHODS

    public Transform GetPoolElement(PoolType poolType)
    {
        try
        {
            return poolDataList.Find(x => x.poolType == poolType).prefab;
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("Missing Prefab of {0}!!!", poolType);
            return null;
        }
    }

    #endregion

}
