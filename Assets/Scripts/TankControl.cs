using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : TankBehaviour
{
    [SerializeField] private InputData _input;
    
    // Start is called before the first frame update
    void Start()
    {
        Initialize();   
    }

    // Update is called once per frame
    private void Update()
    {
        FireTimer();

        if (inFire) return;
        LoopControl();
    }

    void FixedUpdate()
    {
        LoopBehaviour();
    }

    void ControlInput()
    {
        moveDirection = Input.GetAxis(_input.verticalMoveAxis);
        turnDirection = Input.GetAxis(_input.horizontalMoveAxis);

        if (Input.GetKeyDown(_input.fire))
        {
            Fire();
        }
    }

    void LoopBehaviour()
    {
        TankMove();
        RotateTank();
    }

    void LoopControl()
    {
        ControlInput();
    }
}
