using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Key> collectedKeys;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            LevelManager.instance.RespawnPlayer();
        }
        else if (collision.CompareTag("RoomEntrance"))
        {
            LevelManager.instance.NextRoom();
        }
    }
}
