using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TowerBehaviour : MonoBehaviour
{
    public static TowerBehaviour instance;

    [SerializeField] private Slider captureBar;
    [SerializeField] private float timeCapture;
    [SerializeField] private float maxValue;
    [SerializeField] private float incrementValue;
    [SerializeField] private UnityEvent eventTrigger;

    private float redFieldValue;
    private float blueFieldValue;
    private float time;
    private bool redTeam, blueTeam;


    private void SetInitializeField()
    {
        captureBar.maxValue = maxValue;
        captureBar.value = maxValue / 2;
        redFieldValue = maxValue / 2;
        blueFieldValue = maxValue / 2;
        instance = this;
    }

    private void Start()
    {
        SetInitializeField();
    }

    private void Update()
    {
        if (GameManagement.gameOver) return;
        CapturingTime();
    }

    private void GetCaptured()
    {
        if (blueFieldValue == maxValue)
        {
            UIManager.instance.SetGameOverUI("Blue");
        }

        if (redFieldValue == maxValue)
        {
            UIManager.instance.SetGameOverUI("Red");
        }

        eventTrigger.Invoke();
        GameManagement.instance.GameOver();
    }

    private void CapturingTime()
    {
        if (redTeam && !blueTeam)
        {
            CapturingField("RED");
        }
        else if (!redTeam && blueTeam)
        {
            CapturingField("BLUE");
        } else
        {
            time = 0;
        }
    }

    private void CapturingField(string fieldName)
    {
        if (time > timeCapture)
        {
            SetField(fieldName);
            time = 0;
        } else
        {
            time += Time.deltaTime;
        }
    }

    private void SetField(string fieldName)
    {
        switch(fieldName.ToLower())
        {
            case "red":
                UpdateRedField(incrementValue);
                UpdateBlueField(-incrementValue);
                break;
            case "blue":
                UpdateRedField(-incrementValue);
                UpdateBlueField(incrementValue);
                break;
        }

        UpdateBarField();
    }

    private void UpdateRedField(float value)
    {
        redFieldValue += value;

        if (redFieldValue < 0)
        {
            redFieldValue = 0;
        } 
        else if (redFieldValue > maxValue)
        {
            redFieldValue = maxValue;
            GetCaptured();
        }
    }

    private void UpdateBlueField(float value)
    {
        blueFieldValue += value;

        if (blueFieldValue < 0)
        {
            blueFieldValue = 0;
        }
        else if (blueFieldValue > maxValue)
        {
            blueFieldValue = maxValue;
            GetCaptured();
        }
    }

    private void UpdateBarField()
    {
        captureBar.value = redFieldValue;
    }

    public void SetTankInArea(string command, bool flag)
    {
        switch (command.ToLower())
        {
            case "player 1":
                redTeam = flag;
                break;
            case "player 2":
                blueTeam = flag;
                break;
        }
    }
}
