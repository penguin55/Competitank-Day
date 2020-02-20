using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    private Vector3 direction;
    private Vector3 desireDirection;

    private void OnDisable()
    {
        direction = Vector3.zero;
    }

    public void Initialize(Vector3 setDirection)
    {
        direction = setDirection;
    }

    void Update()
    {
        BulletMove();
    }

    void BulletMove()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            desireDirection = Vector3.Reflect(direction, collision.contacts[0].normal);
            direction = desireDirection;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    float GetAngleCollision()
    {
        return 0;
    }
}
