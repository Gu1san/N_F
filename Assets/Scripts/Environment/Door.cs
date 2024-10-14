using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] BoxCollider2D doorCollider;
    void Start()
    {

    }

    public override void Activate()
    {
        doorCollider.enabled = false;
    }

    public override void Deactivate()
    {
        doorCollider.enabled = true;
    }

    public override void Reset()
    {
        doorCollider.enabled = true;
    }
}
