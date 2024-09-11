using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayers : MonoBehaviour
{
    [SerializeField] GameObject NamiPrefab;
    [SerializeField] GameObject FloraPrefab;
    private Vector3 currentPosition;
    GameObject activePlayer;

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
            if (activePlayer == NamiPrefab)
            {
                FloraPrefab.transform.position = currentPosition;
                activePlayer = FloraPrefab;
            }
            else
            {
                NamiPrefab.transform.position = currentPosition;
                activePlayer = NamiPrefab;
            }
            NamiPrefab.SetActive(!NamiPrefab.activeInHierarchy);
            FloraPrefab.SetActive(!FloraPrefab.activeInHierarchy);
        }
    }
}
