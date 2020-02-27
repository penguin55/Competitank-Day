using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;

    public bool gameOver;

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Transform player1Spawn;
    [SerializeField] private Transform player2Spawn;
    [SerializeField] private float timeRespawn;

    

    private void Start()
    {
        instance = this;
        gameOver = false;
    }

    public void Respawn(string playerID)
    {
        if (!gameOver)
        {
            StartCoroutine(respawning(playerID));
        }
    }

    IEnumerator respawning(string playerID)
    {
        yield return new WaitForSeconds(timeRespawn);
        switch (playerID.ToLower())
        {
            case "player 1":
                player1.transform.position = player1Spawn.position;
                player1.SetActive(true);
                break;
            case "player 2":
                player2.transform.position = player2Spawn.position;
                player2.SetActive(true);
                break;
        }
    }
}
