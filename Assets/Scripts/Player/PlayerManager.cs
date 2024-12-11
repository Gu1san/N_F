using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Key> collectedKeys;
    public CameraShake cameraShake;

    private PlayerMovement movement;
    private Rigidbody2D rb;

    private void Start() {
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            FreezePlayer(true);
            await cameraShake.Shake();
            LevelManager.instance.RespawnPlayer();
            FreezePlayer(false);
        }
        else if (collision.CompareTag("RoomEntrance"))
        {
            LevelManager.instance.NextRoom();
        }
    }

    void FreezePlayer(bool freeze){
        movement.enabled = !freeze;
        rb.bodyType = freeze ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
    }
}
