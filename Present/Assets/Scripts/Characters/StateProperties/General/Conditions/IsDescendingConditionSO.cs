using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/General/Is Descending")]
public class IsDescendingConditionSO : StateConditionSO<IsDescendingCondition>
{
    public float descendingThreashold = -2;
}

public class IsDescendingCondition : Condition
{
    private Character _character;
    private IsDescendingConditionSO _originSO => (IsDescendingConditionSO)base.OriginSO; // The SO this Condition spawned from
    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    protected override bool Statement()
    {
        return _character.movementVector.y < _originSO.descendingThreashold;
    }
}
