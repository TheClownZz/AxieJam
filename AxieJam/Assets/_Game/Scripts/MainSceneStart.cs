using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneStart : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.player.gameObject.SetActive(true);
        GameManager.Instance.map.gameObject.SetActive(true);
    }


}
