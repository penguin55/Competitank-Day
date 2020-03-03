using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Slider player1Health;
    [SerializeField] private Slider player2Health;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text winText;
    [SerializeField] private Color redTeamColor;
    [SerializeField] private Color blueTeamColor;
    [SerializeField] private Text turretPlayer1_Text;
    [SerializeField] private Text cameraPlayer1_Text;
    [SerializeField] private Text turretPlayer2_Text;
    [SerializeField] private Text cameraPlayer2_Text;

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

    public void SetGameOverUI(string team)
    {
        gameOverPanel.SetActive(true);
        winText.text = team+" Won";
        switch (team.ToLower())
        {
            case "red":
                winText.color = redTeamColor;
                break;
            case "blue":
                winText.color = blueTeamColor;
                break;
        }
    }

    public void SendMessageToSkilUI(string value, string commandType, string playerID, bool flag)
    {
        switch ((commandType+"-"+playerID).ToLower())
        {
            case "turret-player 1" :
                turretPlayer1_Text.color = flag ? Color.white : Color.red;
                turretPlayer1_Text.text = value;
                break;
            case "turret-player 2":
                turretPlayer2_Text.color = flag ? Color.white : Color.red;
                turretPlayer2_Text.text = value;
                break;
            case "camera-player 1":
                cameraPlayer1_Text.color = flag ? Color.white : Color.red;
                cameraPlayer1_Text.text = value;
                break;
            case "camera-player 2":
                cameraPlayer2_Text.color = flag ? Color.white : Color.red;
                cameraPlayer2_Text.text = value;
                break;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        SceneManager.LoadScene("MAIN_MENU");
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
