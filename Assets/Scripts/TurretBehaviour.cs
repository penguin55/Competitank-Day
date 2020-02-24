using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{

    [SerializeField] private Transform turret;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject bullets;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private Transform bulletDirection;
    [SerializeField] private float fireRate;

    private Transform target;
    private Vector3 targetNormalize;
    private Quaternion toRotation;
    private float timeFireRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateTo();
        Fire();
    }

    protected void RotateTo()
    {
        if (target)
        {
            targetNormalize = target.position - turret.position;
            targetNormalize.y = 0;
            toRotation = Quaternion.LookRotation(targetNormalize);
            turret.rotation = Quaternion.Lerp(turret.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    protected void Fire()
    {
        if (target == null) return;
        if (timeFireRate > fireRate)
        {
            GameObject bulletInstance = SharedPoolingObject.instance.GetObject("Turret-Bullet");
            if (bulletInstance != null)
            {
                bulletInstance.transform.parent = null;
                bulletInstance.SetActive(true);
                bulletInstance.transform.position = bulletSpawnPosition.position;
                bulletInstance.GetComponent<Bullet>().SetDirection(GetDirectionFire().normalized, turret.eulerAngles.y);
                bulletInstance.GetComponent<Bullet>().SetOwner("turret");

                timeFireRate = 0;
            }
            
        } else
        {
            timeFireRate += Time.deltaTime;
        }
    }

    private Vector3 GetDirectionFire()
    {
        return bulletDirection.position - bulletSpawnPosition.position;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Player"))
        {
            target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }
}
