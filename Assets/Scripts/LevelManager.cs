using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] roomEntrances;
    [SerializeField] ChangePlayers player;
    public delegate void OnRestart();
    public event OnRestart onRestart;
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
        RespawnPlayer(false);
    }

    public void RespawnPlayer(bool resetScene = true)
    {
        player.NamiPrefab.transform.position = spawnPoints[currentRoom].position;
        player.FloraPrefab.transform.position = spawnPoints[currentRoom].position;
        if(resetScene) onRestart?.Invoke();
    }

    public void NextRoom()
    {
        if(currentRoom < spawnPoints.Length - 1)
        {
            roomEntrances[currentRoom].SetActive(false);
            currentRoom++;
            Debug.Log("Next room");
        }
        else
        {
            Debug.Log("�ltima sala da fase");
        }
    }
}
