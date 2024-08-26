using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using static CombatDamageUtilities;

[CreateAssetMenu(fileName = "EnemyAttackHitbox", menuName = "State Machines/Actions/Enemies/AttackHitBox")]
public class GeneralEnemyAttackHitboxActionSO : StateActionSO<GeneralEnemyAttackHitboxAction>
{
    public Rect hitbox;
    public AbilityDataSO abilityData;
}

public class GeneralEnemyAttackHitboxAction : StateAction
{
    private Vector2 offset;

    private Collider2D[] detected;
    protected Movement Movement
    {
        get => movement ?? _npc.Core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    private NonPlayerStatsManager _stats;

    private GeneralEnemyAttackHitboxActionSO _originSO => (GeneralEnemyAttackHitboxActionSO)base.OriginSO;

    private NonPlayerCharacter _npc;

    public override void Awake(StateMachine stateMachine)
    {
        _npc = stateMachine.GetComponent<NonPlayerCharacter>();
        _stats = stateMachine.GetComponent<NonPlayerStatsManager>();
    }


    public override void OnStateEnter()
    {
        _npc.animationEventHandler.OnAttackAction += ActivateHitBox;
    }

    public override void OnStateExit()
    {
        _npc.animationEventHandler.OnAttackAction -= ActivateHitBox;
    }

    private void ActivateHitBox()
    {
        offset.Set(
             _npc.transform.position.x + (_originSO.hitbox.center.x * Movement.FacingDirection),
             _npc.transform.position.y + _originSO.hitbox.center.y);

        detected = Physics2D.OverlapBoxAll(offset, _originSO.hitbox.size, 0f, _npc.entityData.whatIsPlayer);

        TryDamage(detected, new DamageData(_stats.currentStatsSO.CurrentAttack, _stats.currentStatsSO.CurrentArmorIgnore, _stats.currentStatsSO.CurrentMRIgnore, _originSO.abilityData, _npc.entityData.type, _npc.gameObject), out _);
    }
    public override void OnUpdate()
    {
    }
}
