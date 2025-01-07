using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayers : MonoBehaviour
{
    public static ChangePlayers instance;
    public GameObject NamiPrefab;
    public GameObject FloraPrefab;
    [SerializeField] CameraFollowObject cameraFollow;
    private Vector3 currentPosition;
    public GameObject activePlayer;
    public Key collectedKey;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

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
            currentPosition = activePlayer.transform.position;
            PlayerMovement playerMov = activePlayer.GetComponent<PlayerMovement>();
            if(!playerMov.canSwitch) return;
            activePlayer = activePlayer == NamiPrefab ? FloraPrefab : NamiPrefab;
            activePlayer.transform.position = currentPosition;
            cameraFollow.ChangePlayer();
            NamiPrefab.SetActive(!NamiPrefab.activeInHierarchy);
            FloraPrefab.SetActive(!FloraPrefab.activeInHierarchy);
            if(collectedKey != null)collectedKey.target = activePlayer.transform.GetChild(0);
        }
    }
}
