using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttom : MonoBehaviour
{
    [SerializeField] MovingPlatform platform;

    public void OnPress()
    {
        platform.Activate();
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
