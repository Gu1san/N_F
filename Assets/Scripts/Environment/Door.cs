using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    BoxCollider2D[] doorColliders;
    void Start()
    {
        doorColliders = GetComponentsInChildren<BoxCollider2D>();
    }

    public override void Activate()
    {
        foreach (BoxCollider2D collider in doorColliders)
        {
            collider.enabled = false;
        }
    }

    public override void Deactivate()
    {
        foreach (BoxCollider2D collider in doorColliders)
        {
            collider.enabled = true;
        }
    }

    public override void Reset()
    {
        Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent(out PlayerManager p))
            {
                foreach(Key key in p.collectedKeys)
                {
                    if(key.targetDoor == this)
                    {
                        key.Activate();
                        Activate();
                    }
                }
            }
        }
    }
}
