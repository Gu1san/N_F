using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable: MonoBehaviour
{
    public abstract void Activate();
    public abstract void Deactivate();
    public abstract void Reset();
}
