using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntrance : MonoBehaviour
{
    [SerializeField] BoxCollider2D[] colliders;
    [SerializeField] BoxCollider2D trigger;
    public void EnableCollider(){
        trigger.enabled = false;
        colliders[0].enabled = false;
        colliders[1].enabled = true;
    }
}
