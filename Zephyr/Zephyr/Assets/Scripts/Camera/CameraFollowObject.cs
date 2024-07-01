using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private TransformAnchor _playerTransformAnchor;
    [SerializeField] private float _flipRotationTime = 0.5f;
    [SerializeField] private CheckIfShouldFlipSO CheckIfShouldFlip;
    [SerializeField] private Player _player;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnEnable()
    {
        CheckIfShouldFlip.FlipEvent += CallTurn;
    }

    private void OnDisable()
    {
        CheckIfShouldFlip.FlipEvent -= CallTurn;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = _playerTransformAnchor.Value.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 30 * Time.deltaTime);
    }

    public void CallTurn()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), _flipRotationTime).setEaseInOutSine();
    }

    private float DetermineEndRotation()
    {
        if (_player.facingDirection == -1)
        {
            return 0f;
        }
        else
        {
            return 180f;
        }
    }
}
