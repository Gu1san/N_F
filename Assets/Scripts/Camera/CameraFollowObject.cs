using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] float flipYRotationTime = .5f;
    [SerializeField] float transitionTime = 0.5f;

    List<PlayerMovement> player = new();

    int index = 0;

    bool isFacingRight;
    bool isTransitioning;

    private void Start()
    {
        player.Add(playerTransform.gameObject.GetComponent<PlayerMovement>());
        isFacingRight = player[0].isFacingRight;
    }

    void Update()
    {
        if(!isTransitioning)
            transform.position = playerTransform.position;
    }

    public void ChangePlayer(PlayerMovement newPlayer){
        if(player.Count <= 1){
            player.Add(newPlayer);
        }

        index = (index+1) % player.Count;
        StartCoroutine(PlayerTransition(player[index].transform));
        playerTransform = player[index].transform;

        if(player[index].isFacingRight != isFacingRight){
            CallTurn();
        }
    }

    public void CallTurn()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), flipYRotationTime).setEaseInOutSine();
    }

    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;

        if (isFacingRight)
        {
            return 0;
        }
        else
        {
            return 180;
        }
    }

    IEnumerator PlayerTransition(Transform newPlayerTransform)
    {
        isTransitioning = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = newPlayerTransform.position;

        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        playerTransform = newPlayerTransform;
        isTransitioning = false;
    }
}
