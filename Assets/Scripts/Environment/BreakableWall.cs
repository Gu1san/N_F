using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : Interactable
{
    Collider2D[] colliders;

    private void Start()
    {
        colliders = GetComponents<Collider2D>();
    }

    public override void Activate()
    {
        foreach (Collider2D c in colliders) { c.enabled = false; }
        LevelManager.instance.onRestart += Reset;
    }

    public override void Deactivate()
    {
        
    }

    public override void Reset()
    {
        foreach (Collider2D c in colliders) { c.enabled = true; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Nami"))
            return;

        if (collision.TryGetComponent(out PlayerMovement playerMovement))
        {
            if (playerMovement.isDashing)
            {
                Activate();
            }
        }
    }
}
