using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
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
        else if (collision.CompareTag("Button"))
        {
            if(collision.gameObject.TryGetComponent(out Buttom b))
            {
                b.Activate();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Button"))
        {
            if (collision.gameObject.TryGetComponent(out Buttom b))
            {
                b.Deactivate();
            }
        }
    }
}
