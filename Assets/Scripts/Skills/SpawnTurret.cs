using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AddSkill/Turret", fileName = "Turret")]
public class SpawnTurret : ScriptableObject
{
    public string nameSkill;
    public float durationSkill;
    [SerializeField] private GameObject prefabs;
    public void UseSkill(PlayerInfo info, GameObject player, SkillControl skillControl)
    {
        GameObject turret = Instantiate(prefabs, player.transform.position, Quaternion.identity);
        turret.GetComponent<TurretBehaviour>().Initialize(player, skillControl);
    }
}
