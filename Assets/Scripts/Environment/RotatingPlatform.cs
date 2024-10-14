using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : Interactable
{
    [SerializeField] float speed = 3;
    [SerializeField] bool autoActivate;
    int currentIndex;
    Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
        LevelManager.instance.onRestart += Reset;
        if (autoActivate) Activate();
    }

    public override void Activate()
    {
        StartCoroutine(Rotate());
    }

    public override void Deactivate()
    {
        StopAllCoroutines();
    }

    public override void Reset()
    {
        StopAllCoroutines();
        transform.rotation = startRotation;
    }

    IEnumerator Rotate()
    {
        transform.Rotate(0, 0, 1 * speed * Time.deltaTime);
        yield return null;
        StartCoroutine(Rotate());
    }
}
