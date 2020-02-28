using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCameraManagement : MonoBehaviour
{
    public static TPSCameraManagement instance;

    [SerializeField] private GameObject cameraPlayer1;
    [SerializeField] private GameObject cameraPlayer2;

    private void Start()
    {
        instance = this;
    }

    public void SetActiveTPSCamera(string cameraID, bool flag)
    {
        switch (cameraID.ToLower())
        {
            case "player 1" :
                cameraPlayer1.SetActive(flag);
                break;
            case "player 2":
                cameraPlayer2.SetActive(flag);
                break;
        }
    }

}
