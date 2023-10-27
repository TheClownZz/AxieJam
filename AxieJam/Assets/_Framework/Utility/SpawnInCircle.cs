using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;


public class SpawnInCircle : MonoBehaviour
{
    [SerializeField] float radius = 1f;
    [SerializeField] List<Transform> tfList;


    [Button]
    private void Spawn()
    {
        for (int i = 0; i < tfList.Count; i++)
        {
            float angle = i * Mathf.PI * 2f / tfList.Count;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, tfList[i].position.y, Mathf.Sin(angle) * radius);
            tfList[i].position = newPos;
        }
    }

}
