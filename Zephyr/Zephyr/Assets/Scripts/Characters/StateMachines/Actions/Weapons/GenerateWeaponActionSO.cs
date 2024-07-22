using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "GenerateWeaponAction", menuName = "State Machines/Actions/GenerateWeapon")]
public class GenerateWeaponActionSO : StateActionSO
{
    public CombatInputs weaponIndex;
    protected override StateAction CreateAction() => new GenerateWeaponAction(weaponIndex);
}

public class GenerateWeaponAction : StateAction
{
    private CombatInputs _weaponIndex;

    private Player _player;
    private Weapon _weapon;
    private WeaponGenerator _weaponGenerator;
    public GenerateWeaponAction(CombatInputs weaponIndex)
    {
        _weaponIndex = weaponIndex;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        _weapon = _player.weapons[(int)_weaponIndex];
        _weaponGenerator = _weapon.GetComponent<WeaponGenerator>();
        _weapon.EventHandler.OnFinish += HandleFinish;
        _weapon.OnUseInput += HandleUseInput;

        _weapon.Enter();
    }

    private void HandleFinish()
    {
        _player.isAbilityFinished = true;
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnStateExit()
    {
        _weapon.Exit();
    }

    private void HandleUseInput() => _player.UseAttackInput((int)_weaponIndex);
}
