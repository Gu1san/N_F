using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayers : MonoBehaviour
{
    public GameObject NamiPrefab;
    public GameObject FloraPrefab;
    [SerializeField] CameraFollowObject cameraFollow;
    private Vector3 currentPosition;
    public GameObject activePlayer;

    private void Start()
    {
        NamiPrefab.SetActive(true);
        FloraPrefab.SetActive(false);
        activePlayer = NamiPrefab;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            activePlayer = activePlayer == NamiPrefab ? FloraPrefab : NamiPrefab;
            cameraFollow.ChangePlayer(activePlayer.GetComponent<PlayerMovement>());
            NamiPrefab.SetActive(!NamiPrefab.activeInHierarchy);
            FloraPrefab.SetActive(!FloraPrefab.activeInHierarchy);
        }
    }
}
