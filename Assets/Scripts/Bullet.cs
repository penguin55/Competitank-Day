using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed; 
    [SerializeField] private int maxHit;
    
    private Rigidbody rigid;
    private int currentHit;
    private Vector3 direction;
    private Vector3 desireDirection;

    public void Initialize()
    {
        rigid = GetComponent<Rigidbody>();
        direction = Vector3.zero;
        desireDirection = Vector3.zero;
        rigid.velocity = Vector3.zero;
    }

    public void SetDirection(Vector3 setDirection, float angle)
    {
        direction = setDirection;
        transform.rotation = Quaternion.Euler(0f, angle ,90f);
    }

    void Update()
    {
        BulletMove();
    }

    void BulletMove()
    {
        rigid.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            desireDirection = Vector3.Reflect(direction, collision.contacts[0].normal);
            Quaternion angleAdjustment = Quaternion.FromToRotation(direction, desireDirection);
            transform.rotation = angleAdjustment * transform.rotation;
            direction = desireDirection;
            CountHit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        SharedPoolingObject.instance.DeactiveObject(gameObject);
    }

    private void CountHit()
    {
        currentHit++;
        if (currentHit >= maxHit)
        {
            currentHit = 0;
            DestroyObject();
        }
    }

    private void OnEnable()
    {
        currentHit = 0;
    }

    private void OnDisable()
    {
        direction = Vector3.zero;
        desireDirection = Vector3.zero;
        rigid.velocity = Vector3.zero;
    }
}
