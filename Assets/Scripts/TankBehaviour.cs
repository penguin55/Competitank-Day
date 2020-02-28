using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehaviour : MonoBehaviour
{
    protected float moveDirection;
    protected float turnDirection;
    protected bool moveable;
    protected Rigidbody rigid;
    protected bool inFire;
    
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private Transform bulletDirection;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireKnockbackPower;
    [SerializeField] private int ammo;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private float turnTemp;
    private float fireTime;
    private Quaternion rotationTemp;
    private Vector3 direction;
    private int maxHealth;
    private int health;
    private string playerID;

    protected void ResetData()
    {
        moveable = true;
        fireTime = fireRate;
        inFire = false;
        health = maxHealth;
    }

    protected void Initialize(string playerID)
    {
        rigid = GetComponent<Rigidbody>();
        moveable = true;
        fireTime = fireRate;
        inFire = false;
        this.playerID = playerID;
    }

    protected void SetHealth(int health)
    {
        maxHealth = health;
        this.health = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
            Dead();
        }
        UIManager.instance.UpdateBarHeath(playerID, health);
    }
    
    protected void TankMove()
    {
        direction = transform.forward * moveDirection * moveSpeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + direction);
    }

    protected void RotateTank()
    {
        turnTemp = turnDirection * rotateSpeed * Time.deltaTime;
        rotationTemp = Quaternion.Euler(0f, turnTemp , 0f);
        rigid.MoveRotation(rigid.rotation * rotationTemp);
    }

    protected void Fire()
    {
        if (fireTime >= fireRate)
        {
            GameObject bulletInstance = SharedPoolingObject.instance.GetObject("Tank-Bullet");
            if (bulletInstance != null)
            {
                bulletInstance.transform.parent = null;
                bulletInstance.SetActive(true);
                bulletInstance.transform.position = bulletSpawnPosition.position;
                bulletInstance.GetComponent<Bullet>().SetDirection(GetDirectionFire().normalized, transform.eulerAngles.y);
                bulletInstance.GetComponent<Bullet>().SetOwner("tank");

                fireTime = 0;
                inFire = true;
                ResetMovement();
                FireKnockback();
            }
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
        GameManagement.instance.Respawn(playerID);
        ResetSkill();
    }

    private Vector3 GetDirectionFire()
    {
        return bulletDirection.position - bulletSpawnPosition.position;
    }

    protected void FireTimer()
    {
        if (fireTime < fireRate)
        {
            fireTime += Time.deltaTime;
        } else
        {
            inFire = false;
        }
    }

    private void FireKnockback()
    {
        transform.Translate(-Vector3.forward * fireKnockbackPower * Time.deltaTime);
    }

    private void ResetMovement()
    {
        moveDirection = 0;
        turnDirection = 0;
    }

    private void ResetSkill()
    {
        TPSCameraManagement.instance.SetActiveTPSCamera(playerID, false);
    }
}
