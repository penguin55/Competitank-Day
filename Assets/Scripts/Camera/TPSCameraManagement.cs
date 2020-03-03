using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCameraManagement : MonoBehaviour
{
    public static TPSCameraManagement instance;

    [SerializeField] private GameObject cameraPlayer1;
    [SerializeField] private GameObject cameraPlayer2;
    [SerializeField] private SkillControl skillControl1;
    [SerializeField] private SkillControl skillControl2;
    [SerializeField] private float durationChargeCam1;
    [SerializeField] private float durationChargeCam2;

    private float durationSkillCam1;
    private float durationSkillCam2;
    private float chargeTimeCam1;
    private float chargeTimeCam2;

    private void Start()
    {
        instance = this;
        chargeTimeCam1 = durationChargeCam1;
        chargeTimeCam2 = durationChargeCam2;
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

    public void SetDuration(string cameraID, float durationSkill)
    {
        switch (cameraID.ToLower())
        {
            case "player 1":
                durationSkillCam1 = durationSkill;
                break;
            case "player 2":
                durationSkillCam2 = durationSkill;
                break;
        }
        
    }

    private void Update()
    {
        if (GameManagement.gameOver) return;
        DurationSkillTimming();
        DurationChargeTimming();
    }

    private void DurationSkillTimming()
    {
        if (cameraPlayer1.activeSelf)
        {
            durationSkillCam1 -= Time.deltaTime;
            UpdateTimeUI(((int) durationSkillCam1).ToString(), "player 1", true);
        }
        if (cameraPlayer2.activeSelf)
        {
            durationSkillCam2 -= Time.deltaTime;
            UpdateTimeUI(((int) durationSkillCam2).ToString(), "player 2", true);
        }

        if (cameraPlayer1.activeSelf && durationSkillCam1 < 0)
        {
            durationSkillCam1 = 0;
            chargeTimeCam1 = 0;
            SetActiveTPSCamera("player 1", false);
            UpdateTimeUI(((int) durationSkillCam1).ToString(), "player 1", true);
            UpdateTimeUI("", "player 1", true);
        }

        if (cameraPlayer2.activeSelf && durationSkillCam2 < 0)
        {
            durationSkillCam2 = 0;
            chargeTimeCam2 = 0;
            SetActiveTPSCamera("player 2", false);
            UpdateTimeUI(((int) durationSkillCam2).ToString(), "player 2", true);
            UpdateTimeUI("", "player 2", true);
        }
    }

    private void UpdateTimeUI(string value, string player_id, bool flag)
    {
        UIManager.instance.SendMessageToSkilUI(value, "camera", player_id, flag);
    }

    private void DurationChargeTimming()
    {
        if (!cameraPlayer1.activeSelf && chargeTimeCam1 < durationChargeCam1)
        {
            chargeTimeCam1 += Time.deltaTime;
            UpdateTimeUI(((int) chargeTimeCam1).ToString(), "player 1", false);
        }
        if (!cameraPlayer2.activeSelf && chargeTimeCam2 < durationChargeCam2)
        {
            chargeTimeCam2 += Time.deltaTime;
            UpdateTimeUI(((int) chargeTimeCam2).ToString(), "player 2", false);
        }

        if (!cameraPlayer1.activeSelf && chargeTimeCam1 > durationChargeCam1)
        {
            chargeTimeCam1 = durationChargeCam1;
            skillControl1.CanSkillCamera();
            UpdateTimeUI(((int) chargeTimeCam1).ToString(), "player 1", false);
            UpdateTimeUI("", "player 1", true);
        }

        if (!cameraPlayer2.activeSelf && chargeTimeCam2 > durationChargeCam2)
        {
            chargeTimeCam2 = durationChargeCam2;
            skillControl2.CanSkillCamera();
            UpdateTimeUI(((int) chargeTimeCam2).ToString(), "player 2", false);
            UpdateTimeUI("", "player 2", true);
        }
    }

}
