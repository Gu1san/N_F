using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] allVirtualCameras;

    [Header("Controls for lerping Y during fall/jump")]
    [SerializeField] private float fallPanAmount = .25f;
    [SerializeField] private float fallYPanTime = .35f;
    public float fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping { get; private set; }
    public bool LerpedFromPlayerFalling { get; set; }

    private CinemachineVirtualCamera currentCamera;
    private CinemachineFramingTransposer framingTransposer;

    private float normYPanAmount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        for(int i = 0; i < allVirtualCameras.Length; i++)
        {
            if (allVirtualCameras[i].enabled)
            {
                currentCamera = allVirtualCameras[i];

                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

        normYPanAmount = framingTransposer.m_YDamping;
    }

    public void LerpYDamping(bool isPlayerFalling)
    {
        StartCoroutine(LerpYAction(isPlayerFalling));
    }

    IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        float startDampAmount = framingTransposer.m_YDamping;
        float endDampAmount;

        if (isPlayerFalling)
        {
            endDampAmount = fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = normYPanAmount;
        }

        float elapsedTime = 0;
        while(elapsedTime < fallYPanTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / fallYPanTime));
            framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }

        IsLerpingYDamping = false;
    }
}
