using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public Door targetDoor;
    private PlayerManager player;
    private Transform target;
    [SerializeField]CircleCollider2D circleCollider;
    [SerializeField] float speed = 4;
    Vector2 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    public override void Activate()
    {
        player.collectedKeys.Add(this);
        StartCoroutine(FollowPlayer());
        circleCollider.enabled = false;
    }

    public override void Deactivate()
    {
        StopAllCoroutines();
        player.collectedKeys.Remove(this);
    }

    IEnumerator FollowPlayer()
    {
        transform.position = Vector2.Lerp(transform.position, target.position, speed * Time.deltaTime);
        yield return null;
        StartCoroutine(FollowPlayer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out PlayerManager p))
            {
                player = p;
                target = player.transform.GetChild(0);
                Activate();
            }
        }
    }

    public override void Reset()
    {
        StopAllCoroutines();
        transform.position = startPosition;
        circleCollider.enabled = true;
    }
}
