using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] ChangePlayers player;
    private int currentRoom;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player.transform.position = spawnPoints[currentRoom].transform.position;
    }

    public void RespawnPlayer()
    {
        player.activePlayer.transform.position = spawnPoints[currentRoom].position;
    }

    public void NextRoom()
    {
        if(currentRoom < spawnPoints.Length - 1)
        {
            currentRoom++;
            RespawnPlayer();
        }
        else
        {
            Debug.Log("Última sala da fase");
        }
    }
}
