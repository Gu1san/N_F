using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : Interactable
{
    Collider2D wallCollider;

    private void Start()
    {
        wallCollider = GetComponent<Collider2D>();
    }

    public override void Activate()
    {
        wallCollider.enabled = false;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        LevelManager.instance.onRestart += Reset;
    }

    public override void Deactivate()
    {
        if(wallCollider.enabled){
            wallCollider.isTrigger = true;
        }
    }

    public override void Reset()
    {
        wallCollider.enabled = true;
        wallCollider.isTrigger = false;
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
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
