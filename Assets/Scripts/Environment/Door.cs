using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    BoxCollider2D[] doorColliders;
    [SerializeField] Color targetColor;
    private Key key;
    void Start()
    {
        doorColliders = GetComponentsInChildren<BoxCollider2D>();
    }

    public override void Activate()
    {
        GetComponentInChildren<SpriteRenderer>().color = targetColor;
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
                foreach(Key k in p.collectedKeys)
                {
                    if(k.targetDoor == this)
                    {
                        key = k;
                        k.target = transform;
                        Invoke(nameof(DisableKey), .5f);
                    }
                }
            }
        }
    }

    private void DisableKey(){
        Activate();
        key.Deactivate();
    }
}
