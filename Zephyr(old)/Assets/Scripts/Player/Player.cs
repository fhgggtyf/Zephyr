using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, ICharacter
{
    public PlayerStateMachine StateMachine { get; private set; }

    [Inject] private PlayerStateMachineFactory _stateMachineFactory;
    [Inject] private PlayerCapabilityFactory _capabilityFactory;
    [Inject] private PlayerData _playerData;

    [Inject] public List<PlayerCapabilities> capabilities;

    //[SerializeField] private PlayerData _playerData;

    //[SerializeField] private PlayerStateMachineFactory _factory;

    [Inject] private Ground _ground;

    #region Components
    [Inject] public Core Core { get; private set; }
    [Inject] public Animator Anim { get; private set; }
    [Inject] public PlayerInputHandler InputHandler { get; private set; }
    [Inject] public Rigidbody2D RB { get; private set; }
    [Inject] public BoxCollider2D MovementCollider { get; private set; }
    [Inject] public Stats Stats { get; private set; }
    [Inject] public InteractableDetector InteractableDetector { get; private set; }
    public PlayerData PlayerData { get => _playerData; }
    public Ground Ground { get => _ground; }
    #endregion

    #region Other Variables         

    [Inject(Id = "Primary")]
    private Weapon primaryWeapon;
    [Inject(Id = "Secondary")]
    private Weapon secondaryWeapon;

    #endregion

    private void Awake()
    {
        primaryWeapon.SetCore(Core);
        secondaryWeapon.SetCore(Core);

        capabilities.Add(_capabilityFactory.GetJump(this, "JumpAction"));
        capabilities.Add(_capabilityFactory.GetRoll(this, "RollAction"));
        capabilities.Add(_capabilityFactory.GetDash(this, "DashAction"));

    }

    // Start is called before the first frame update
    void Start()
    {

        InputHandler.OnInteractInputChanged += InteractableDetector.TryInteract;
        StateMachine = _stateMachineFactory.CreateStateMachine(this, PlayerData, Core);

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

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();


}

public enum Capability
{
    jump = 0,
    roll = 1,
    dash = 2
}
