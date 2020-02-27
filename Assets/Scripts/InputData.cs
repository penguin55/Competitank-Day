using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Controller", fileName = "Controller")]
public class InputData : ScriptableObject
{
    public PlayerInfo info;
    public string typeController;
    public string horizontalMoveAxis;
    public string verticalMoveAxis;
    public KeyCode skillTurret;
    public KeyCode skillCamera;
    public KeyCode fire;
}

