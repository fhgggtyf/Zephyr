using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{                                                       
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 8f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 75f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;
    private bool desiredRoll;

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        desiredRoll |= input.RetrieveRollInput();
        direction.x = input.RetrieveMoveInput();
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);
    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = body.velocity;
        acceleration =  maxAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;

        if (desiredRoll)
        {
            desiredRoll = false;
            RollAction();
        }
    }

    private void RollAction()
    {
        if (onGround)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            body.velocity = velocity;
            Debug.Log("1");
        }

    }
}
