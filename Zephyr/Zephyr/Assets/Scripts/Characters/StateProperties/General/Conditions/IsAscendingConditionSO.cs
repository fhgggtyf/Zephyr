using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/General/Is Ascending")]
public class IsAscendingConditionSO : StateConditionSO<IsAscendingCondition>
{
    public float ascendingThreshold = 2;
}

public class IsAscendingCondition : Condition
{
    private Character _character;
    private IsAscendingConditionSO _originSO => (IsAscendingConditionSO)base.OriginSO; // The SO this Condition spawned from
    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    protected override bool Statement()
    {
        return _character.movementVector.y > _originSO.ascendingThreshold;
    }
}
