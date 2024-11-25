using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] RoomEntrance[] roomEntrances;
    [SerializeField] ChangePlayers player;
    [SerializeField] CinemachineConfiner2D cameraConfiner;
    [SerializeField] CompositeCollider2D[] cameraColliders;

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
            roomEntrances[currentRoom].EnableCollider();
            currentRoom++;
            cameraConfiner.m_BoundingShape2D = cameraColliders[currentRoom];
            if(player.FloraPrefab.activeInHierarchy){
                player.NamiPrefab.transform.position = spawnPoints[currentRoom].position;
            }else{
                player.FloraPrefab.transform.position = spawnPoints[currentRoom].position;
            }
            Debug.Log("Next room");
        }
        else
        {
            Debug.Log("�ltima sala da fase");
        }
    }
}
