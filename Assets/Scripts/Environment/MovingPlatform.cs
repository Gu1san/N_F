using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed = 3;
    int currentIndex;

    public void Activate()
    {
        StartCoroutine(Move(waypoints[currentIndex]));
    }

    IEnumerator Move(Transform target)
    {
        while(Vector2.Distance(transform.position, target.position) > .1f)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += speed * Time.deltaTime * dir;
            yield return null;
        }
        currentIndex = (currentIndex + 1) % waypoints.Length;
        StartCoroutine(Move(waypoints[currentIndex]));
    }
}
