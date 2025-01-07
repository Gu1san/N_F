using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float duration, magnitude;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }
    
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
