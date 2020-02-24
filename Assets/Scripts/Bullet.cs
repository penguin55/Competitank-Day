using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed; 
    [SerializeField] private int maxHit;
    [SerializeField] private TrailRenderer trailRender;
    
    private Rigidbody rigid;
    private int currentHit;
    private Vector3 direction;
    private Vector3 desireDirection;
    private string owner;
    

    public void Initialize()
    {
        rigid = GetComponent<Rigidbody>();
        direction = Vector3.zero;
        desireDirection = Vector3.zero;
        rigid.velocity = Vector3.zero;
    }

    public void SetOwner(string owner)
    {
        this.owner = owner;
    }

    public void SetDirection(Vector3 setDirection, float angle)
    {
        direction = setDirection;
        transform.rotation = Quaternion.Euler(0f, angle ,90f);
        trailRender.enabled = true;
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

    private void OnCollisionStay(Collision collision)
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
        SharedPoolingObject.instance.DeactiveObject(gameObject, owner);
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
        trailRender.Clear();
        trailRender.enabled = false;
        direction = Vector3.zero;
        desireDirection = Vector3.zero;
        rigid.velocity = Vector3.zero;
    }
}
