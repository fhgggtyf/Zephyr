using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _flipRotationTime = 0.5f;

    private PlayerStateManager _ctx;
    private short _isFacingRight;

    // Start is called before the first frame update
    void Awake()
    {
        _ctx = _playerTransform.gameObject.GetComponent<PlayerStateManager>();
        _isFacingRight = _ctx.IsFacingRight;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _playerTransform.position; 
    }

    public void CallTurn()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), _flipRotationTime).setEaseInOutSine();
    }
    
    private float DetermineEndRotation()
    {
        _isFacingRight = (short)-_isFacingRight;
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
