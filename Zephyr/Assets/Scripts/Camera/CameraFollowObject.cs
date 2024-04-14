using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _flipRotationTime = 0.5f;
    [SerializeField] private Player _ctx;

    private Movement movement;

    protected Movement Movement
    {
        get => movement ?? _ctx.Core.GetCoreComponent(ref movement);
    }

    private int _isFacingRight;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _isFacingRight = Movement.FacingDirection;
        transform.position = _playerTransform.position; 
    }

    public void CallTurn()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), _flipRotationTime).setEaseInOutSine();
    }
    
    private float DetermineEndRotation()
    {
        _isFacingRight = -_isFacingRight;
        if (_isFacingRight == 1)
        {
            return 0f;
        }
        else
        {
            return 180f;
        }
    }
}
