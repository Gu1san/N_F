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
        LevelManager.instance.onRestart += Reset;
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
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Key k = ChangePlayers.instance.collectedKey;
            if(k?.targetDoor == this)
            {
                key = k;
                k.target = transform;
                Invoke(nameof(DisableKey), .5f);
            }
        }
    }

    private void DisableKey(){
        Activate();
        key.Deactivate();
    }
}
