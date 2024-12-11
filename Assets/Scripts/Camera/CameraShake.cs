using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float duration, magnitude;
    public async Task Shake(){
        float elapsed = 0;
        CinemachineBasicMultiChannelPerlin cinemachineChannel = 
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineChannel.m_AmplitudeGain = magnitude;
        while(elapsed < duration){
            elapsed += Time.deltaTime;
            await Task.Yield();
        }
        cinemachineChannel.m_AmplitudeGain = 0;
    }
}
