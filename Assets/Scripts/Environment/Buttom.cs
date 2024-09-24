using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttom : MonoBehaviour
{
    [SerializeField] MovingPlatform platform;
    BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        LevelManager.instance.onRestart += () => { boxCollider.enabled = true; };
    }

    public void OnPress()
    {
        platform.Activate();
        boxCollider.enabled = false;
    }
}
