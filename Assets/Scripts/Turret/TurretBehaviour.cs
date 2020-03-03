using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{

    [SerializeField] private Transform turret;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private Transform bulletDirection;
    [SerializeField] private float fireRate;
    [SerializeField] private float offsetToActiveObject;
    [SerializeField] private Collider coll;
    [SerializeField] private AudioSource audioSource;

    private Transform target;
    private Vector3 targetNormalize;
    private Quaternion toRotation;
    private float timeFireRate;
    private GameObject owner;
    private bool activeCollider;
    private SkillControl skillControl;
    private float durationSkill;
    private bool activeTurret;

    public void Initialize(GameObject owner, SkillControl skillControl, float duration)
    {
        coll.enabled = false;
        activeCollider = false;
        this.owner = owner;
        this.skillControl = skillControl;
        durationSkill = duration;
        activeTurret = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.gameOver) return;
        if (!activeTurret) return;
        DurationTiming();
        CheckDistanceFromOwner();
        RotateTo();
        Fire();
    }

    private void RotateTo()
    {
        if (target)
        {
            targetNormalize = target.position - turret.position;
            targetNormalize.y = 0;
            toRotation = Quaternion.LookRotation(targetNormalize);
            turret.rotation = Quaternion.Lerp(turret.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    private void CheckDistanceFromOwner()
    {
        if (!activeCollider)
        {
            if (Vector3.Distance(owner.transform.position, transform.position) > offsetToActiveObject)
            {
                coll.enabled = true;
                activeCollider = true;
            }
        }
    }
    
    private void Fire()
    {
        if (target == null) return;
        if (!target.parent.gameObject.activeSelf) target = null;
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

                AudioManager.instance.PlayOneShotSFXWithSource(ref audioSource, "shoot-turret");

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

    private void DurationTiming()
    {
        durationSkill -= Time.deltaTime;

        if (durationSkill < 0)
        {
            durationSkill = 0;
            UpdateTimeUI(((int) durationSkill).ToString());
            DestroyTurret();
        } else
        {
            UpdateTimeUI(((int) durationSkill).ToString());
        }
    }

    private void DestroyTurret()
    {
        UpdateTimeUI("");
        activeTurret = false;
        Destroy(gameObject);
    }

    private void UpdateTimeUI(string value)
    {
        UIManager.instance.SendMessageToSkilUI(value, "turret", skillControl.GetPlayer(), true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject != owner)
        {
            target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject != owner)
        {
            target = null;
        }
    }

    private void OnDisable()
    {
        skillControl.CanSkillTurret();
    }
}
