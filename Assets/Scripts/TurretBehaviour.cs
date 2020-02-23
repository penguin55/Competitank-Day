using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{

    [SerializeField] private Transform turret;
    [SerializeField] private float rotateSpeed;

    private Transform target;
    private Vector3 targetNormalize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate2();
    }

    protected void RotateTo()
    {
        if (target)
        {
            targetNormalize = new Vector3(target.position.x, turret.position.y, target.position.z);
            turret.LookAt(targetNormalize);
        }
    }

    protected void Rotate2()
    {
        if (target)
        {
            Vector3 relativePos = target.position - turret.position;
            relativePos.y = 0;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            Debug.Log(toRotation);
            turret.rotation = Quaternion.Lerp(turret.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
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
