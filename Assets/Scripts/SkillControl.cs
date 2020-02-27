using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillControl : MonoBehaviour
{
    [SerializeField] private InputData _input;
    [SerializeField] private SpawnTurret turret;
    [SerializeField] private Skills TPSCamera;
    [SerializeField] private GameObject playerBody;

    private bool canActiveTurretSkill;
    private bool canActiveCameraSkill;

    private void Start()
    {
        canActiveCameraSkill = true;
        canActiveTurretSkill = true;
    }

    private void UseSkill(string command)
    {
        switch(command.ToLower())
        {
            case "turret":
                turret.UseSkill(_input.info, playerBody, this);
                break;
            case "tpscamera":
                //turret.UseSkill(_input.info, playerBody, this);
                break;
        }
    }

    private void Skill()
    {
        if (Input.GetKeyDown(_input.skillTurret) && canActiveTurretSkill)
        {
            UseSkill("Turret");
            canActiveTurretSkill = false;
        }

        if (Input.GetKeyDown(_input.skillCamera) && canActiveCameraSkill)
        {
            UseSkill("tpscamera");
            canActiveCameraSkill = false;
        }
    }

    public void CanSkillTurret()
    {
        canActiveTurretSkill = true;
    }

    public void CanSkillCamera()
    {
        canActiveCameraSkill = true;
    }

    private void Update()
    {
        Skill();
    }

}
