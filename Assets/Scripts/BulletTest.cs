using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{

    public float bulletSpeed;

    void Update()
    {
        BulletMove();
    }

    void BulletMove()
    {
        transform.Translate(transform.forward * bulletSpeed);
    }
}
