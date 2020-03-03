using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : TankBehaviour
{
    [SerializeField] private InputData _input;
    
    // Start is called before the first frame update
    void Start()
    {
        Initialize(_input.info.playerID);
        SetHealth(_input.info.health);
        UIManager.instance.SetHealthValue(_input.info.playerID, _input.info.health);
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManagement.gameOver) return;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            TowerBehaviour.instance.SetTankInArea(_input.typeController, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            TowerBehaviour.instance.SetTankInArea(_input.typeController, false);
        }
    }

    private void OnEnable()
    {
        ResetData();
        UIManager.instance.SetHealthValue(_input.info.playerID, _input.info.health);
    }
}
