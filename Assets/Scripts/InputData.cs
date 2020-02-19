using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Controller", fileName = "Controller")]
public class InputData : ScriptableObject
{
    public string typeController;
    public string horizontalMoveAxis;
    public string verticalMoveAxis;
    public string horizontalRotateAxis;
    public string verticalRotateAxis;
    public KeyCode fire;
}

