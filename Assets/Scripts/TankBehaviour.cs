﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehaviour : MonoBehaviour
{
    protected float moveDirection;
    protected float turnDirection;
    protected bool moveable;
    protected Rigidbody rigid;
    protected bool inFire;

    [SerializeField] private GameObject bullet;
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

    protected void Initialize()
    {
        rigid = GetComponent<Rigidbody>();
        moveable = true;
        fireTime = fireRate;
        inFire = false;
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
            GameObject bulletInstance = Instantiate(bullet, bulletSpawnPosition.position, Quaternion.Euler(0f, transform.eulerAngles.y, 90f));
            bulletInstance.GetComponent<Bullet>().Initialize(GetDirectionFire().normalized);
            fireTime = 0;
            inFire = true;
            ResetMovement();
            FireKnockback();
        }
    }

    private Vector3 GetDirectionFire()
    {
        return bulletDirection.localPosition - bulletSpawnPosition.localPosition;
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

}
