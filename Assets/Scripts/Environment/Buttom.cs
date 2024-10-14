using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttom : Interactable
{
    [SerializeField] Interactable targetObject;
    [SerializeField] bool needHold;
    BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        LevelManager.instance.onRestart += Reset;
    }

    public override void Activate()
    {
        targetObject.Activate();
        boxCollider.enabled = needHold;
    }

    public override void Deactivate()
    {
        if (needHold)
        {
            targetObject.Deactivate();
        }
    }

    public override void Reset()
    {
        boxCollider.enabled = true;
    }
}
