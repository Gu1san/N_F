using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttom : Interactable
{
    [SerializeField] Interactable targetObject;
    [SerializeField] bool needHold;
    BoxCollider2D boxCollider;
    Animator anim;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        LevelManager.instance.onRestart += Reset;
        anim = GetComponent<Animator>();
    }

    public override void Activate()
    {
        anim.SetBool("Pressed", true);
        targetObject.Activate();
        boxCollider.enabled = needHold;
    }

    public override void Deactivate()
    {
        if (needHold)
        {
            anim.SetBool("Pressed", false);
            targetObject.Deactivate();
        }
    }

    public override void Reset()
    {
        boxCollider.enabled = true;
        anim.SetBool("Pressed", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Deactivate();
        }
    }
}
