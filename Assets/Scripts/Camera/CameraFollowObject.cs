using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] float flipYRotationTime = .5f;

    List<PlayerMovement> player = new();

    int index = 0;

    bool isFacingRight;

    private void Start()
    {
        player.Add(playerTransform.gameObject.GetComponent<PlayerMovement>());
        isFacingRight = player[0].isFacingRight;
    }

    void Update()
    {
        transform.position = playerTransform.position;
    }

    public void ChangePlayer(PlayerMovement newPlayer){
        if(player.Count <= 1){
            player.Add(newPlayer);
        }
        index = (index+1) % player.Count;
        playerTransform = player[index].transform;

        if(player[index].isFacingRight != isFacingRight)
            CallTurn();
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
}
