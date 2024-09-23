using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] float flipYRotationTime = .5f;

    PlayerMovement player;

    bool isFacingRight;

    private void Awake()
    {
        player = playerTransform.gameObject.GetComponent<PlayerMovement>();
        isFacingRight = player.isFacingRight;
    }

    void Update()
    {
        transform.position = playerTransform.position;
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
