using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : TankBehaviour
{
    [SerializeField] private InputData _input;

    private float xMove, zMove;
    
    // Start is called before the first frame update
    void Start()
    {
        Initialize();   
    }

    // Update is called once per frame
    void Update()
    {
        LoopBehaviour();
    }

    void ControlInput()
    {
        xMove = Input.GetAxis(_input.horizontalMoveAxis);
        zMove = Input.GetAxis(_input.verticalMoveAxis);

        directionMove = new Vector3(xMove, rigid.velocity.y, zMove);
    }

    void LoopBehaviour()
    {
        ControlInput();
        
        TankMove();
    }
}
