using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private float shakeElapsedTime = 0f;

    private CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    private void Awake()
    {
        Instance = this;
    }

    
    private void Start()
    {
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        VirtualCamera.m_Follow = GameObject.Find("Player").transform;
        if (VirtualCamera != null)
        {
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Update()
    {
        VirtualCamera.m_Follow = GameObject.Find("Player").transform;
        if (shakeElapsedTime > 0)
        {
            shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            virtualCameraNoise.m_AmplitudeGain = 0;
            virtualCameraNoise.m_FrequencyGain = 0;
        }
    }

    public void ShootShake()
    {
        SetShake(3f, .1f);
    }

    public void ExplosionShake()
    {
        SetShake(9f, .4f);
    }

    private void SetShake(float amount, float duration)
    {
        shakeElapsedTime = duration;
        virtualCameraNoise.m_AmplitudeGain = amount;
        virtualCameraNoise.m_FrequencyGain = amount;
    }
}
