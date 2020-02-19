using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehaviour : MonoBehaviour
{
    protected Vector3 directionMove;
    protected bool moveable;

    [SerializeField] private float speed;
    protected Rigidbody rigid;

    protected void Initialize()
    {
        rigid = GetComponent<Rigidbody>();
        moveable = true;
    }
    
    protected void TankMove()
    {
        if (moveable) rigid.velocity = directionMove * speed * Time.deltaTime;
    }

    protected void RotateTank()
    {
        
    }
}
