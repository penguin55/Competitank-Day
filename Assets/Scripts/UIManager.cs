using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Slider player1Health;
    [SerializeField] private Slider player2Health;

    private void Awake()
    {
        instance = this;
    }

    public void SetHealthValue(string command, int health)
    {
        switch (command.ToLower())
        {
            case "player 1":
                player1Health.maxValue = health;
                player1Health.value = health;
                break;
            case "player 2":
                player2Health.maxValue = health;
                player2Health.value = health;
                break;
        }  
    }

    public void UpdateBarHeath(string command, int value)
    {
        switch(command.ToLower())
        {
            case "player 1":
                ChangeValuePlayer1Health(value);
                break;
            case "player 2":
                ChangeValuePlayer2Health(value);
                break;
        }
    }

    private void ChangeValuePlayer1Health(int value)
    {
        player1Health.value = value;
    }

    private void ChangeValuePlayer2Health(int value)
    {
        player2Health.value = value;
    }
}
