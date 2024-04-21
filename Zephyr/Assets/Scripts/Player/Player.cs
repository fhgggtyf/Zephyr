using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    public PlayerStateMachine StateMachine { get; private set; }

    public List<PlayerCapabilities> capabilities;

    [SerializeField] private PlayerData _playerData;

    [SerializeField] private PlayerStateMachineFactory _factory;

    private Ground _ground;

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public Stats Stats { get; private set; }
    public InteractableDetector InteractableDetector { get; private set; }
    public PlayerData PlayerData { get => _playerData; set => _playerData = value; }
    public Ground Ground { get => _ground; set => _ground = value; }
    #endregion

    #region Other Variables         

    private Vector2 workspace;

    private Weapon primaryWeapon;
    private Weapon secondaryWeapon;

    #endregion

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();

        primaryWeapon.SetCore(Core);
        secondaryWeapon.SetCore(Core);

        Ground = GetComponent<Ground>();

        Stats = Core.GetCoreComponent<Stats>();
        InteractableDetector = Core.GetCoreComponent<InteractableDetector>();

        capabilities = new List<PlayerCapabilities>
        {
            new Jump(this, "JumpAction"),
            new Roll(this, "RollAction"),
            new Dash(this, "DashAction")
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        InputHandler.OnInteractInputChanged += InteractableDetector.TryInteract;

        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();

        StateMachine = _factory.CreateStateMachine(this, PlayerData, Core);

    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.UpdateStates();
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdateStates();
    }

    private void OnDestroy()
    {
    }

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();


}

public enum Capability
{
    jump = 0,
    roll = 1,
    dash = 2
}
