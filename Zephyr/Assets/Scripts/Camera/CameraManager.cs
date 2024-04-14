using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager instance;
    [SerializeField] public Player player;

    private Movement movement;

    protected Movement Movement
    {
        get => movement ?? player.Core.GetCoreComponent(ref movement);
    }



    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;

    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;
    private float _fallSpeedYDampingChangeThreshold = -7.5f;

    private bool isLerpingYDamping;
    private bool lerpedFromPlayerFalling;

    private Coroutine _lerpYPanCoroutine;

    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private float _normPanAmount;

    public float FallSpeedYDampingChangeThreshold { get => _fallSpeedYDampingChangeThreshold; set => _fallSpeedYDampingChangeThreshold = value; }
    public bool IsLerpingYDamping { get => isLerpingYDamping; set => isLerpingYDamping = value; }
    public bool LerpedFromPlayerFalling { get => lerpedFromPlayerFalling; set => lerpedFromPlayerFalling = value; }



    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        for(int i = 0; i < _allVirtualCameras.Length; i++)
        {
            if (_allVirtualCameras[i].enabled)
            {
                _currentCamera = _allVirtualCameras[i];
                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }

            _normPanAmount = _framingTransposer.m_YDamping;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Movement.CurrentVelocity.y < FallSpeedYDampingChangeThreshold && !IsLerpingYDamping && !LerpedFromPlayerFalling)
        {
            LerpYDamping(true);
        }

        if (Movement.CurrentVelocity.y >= 0f && !IsLerpingYDamping && lerpedFromPlayerFalling)
        {
            lerpedFromPlayerFalling = false;
            LerpYDamping(false);
        }
    }

    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    } 

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        isLerpingYDamping = true;

        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount;

        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = _normPanAmount;
        }

        float elaspedTime = 0f;

        while (elaspedTime < _fallYPanTime)
        {
            elaspedTime += Time.deltaTime;
            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, elaspedTime / _fallYPanTime);
            _framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }

        isLerpingYDamping = false;

    }
}
