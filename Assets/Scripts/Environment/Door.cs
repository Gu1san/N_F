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
                Key key = null;
                foreach(Key k in p.collectedKeys)
                {
                    if(k.targetDoor == this)
                    {
                        key = k;
                        Activate();
                    }
                }
                if(key != null) key.Deactivate();
            }
        }
    }
}
