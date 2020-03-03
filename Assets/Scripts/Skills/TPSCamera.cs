using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AddSkill/TPSCamera", fileName = "TPSCamera")]
public class TPSCamera : ScriptableObject
{
    public string nameSkill;
    public float durationSkill;
    public void UseSkill(PlayerInfo info)
    {
        TPSCameraManagement.instance.SetActiveTPSCamera(info.playerID, true);
        TPSCameraManagement.instance.SetDuration(info.playerID, durationSkill);
    }
}
