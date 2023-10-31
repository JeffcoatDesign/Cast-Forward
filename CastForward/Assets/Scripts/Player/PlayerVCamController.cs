using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class PlayerVCamController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _vCam;
    public CinemachineVirtualCamera PlayerVCam { get { return _vCam; } }
    [SerializeField] private float targetFov;
    [SerializeField] private float fovChangeSpeed = 0.5f;
    [SerializeField] private float shakeIntesity = 10f;
    [SerializeField] private float shakeTime = 1f;
    private void OnEnable()
    {
        PlayerEntity.OnPlayerHit += ShakeOnHit;
    }
    private void OnDisable()
    {
        PlayerEntity.OnPlayerHit -= ShakeOnHit;
    }
    public void SetCameraShake (float frequency)
    {
        PlayerVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
    }
    public void SetFOV (float FOV)
    {
        PlayerVCam.m_Lens.FieldOfView = FOV;
    }
    public void SetTargetFOV(float FOV)
    {
        targetFov = FOV;
    }
    public void SetDutch(float dutchAngle)
    {
        PlayerVCam.m_Lens.Dutch = dutchAngle;
    }
    private void Update()
    {
        if (PlayerVCam.m_Lens.FieldOfView != fovChangeSpeed)
        {
            PlayerVCam.m_Lens.FieldOfView = Mathf.Lerp(PlayerVCam.m_Lens.FieldOfView, targetFov, fovChangeSpeed * Time.deltaTime);
        }
    }
    private void ShakeOnHit ()
    {
        StartCoroutine("ShakeCamera");
    }
    private IEnumerator ShakeCamera()
    {
        SetCameraShake(shakeIntesity);
        yield return new WaitForSeconds(shakeTime);
        SetCameraShake(0);
    }
}
