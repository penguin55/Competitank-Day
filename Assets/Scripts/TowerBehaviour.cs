using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] private Slider captureBar;
    [SerializeField] private float timeCapture;
    [SerializeField] private float maxValue;
    [SerializeField] private float incrementValue;

    private float redFieldValue;
    private float blueFieldValue;
    private float time;
    [SerializeField] private bool redTeam, blueTeam;


    private void SetInitializeField()
    {
        captureBar.maxValue = maxValue;
        captureBar.value = maxValue / 2;
        redFieldValue = maxValue / 2;
        blueFieldValue = maxValue / 2;
    }

    private void Start()
    {
        SetInitializeField();
    }

    private void Update()
    {
        CapturingTime();
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
        }
    }

    private void UpdateBarField()
    {
        captureBar.value = redFieldValue;
    }
}
