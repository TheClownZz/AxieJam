using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet)
        {
            bullet.Clear();
        }
    }


}
