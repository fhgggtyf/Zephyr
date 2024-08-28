using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/General/Is Hovering")]
public class IsHoveringConditionSO : StateConditionSO<IsHoveringCondition>
{
    public float descendingThreashold = -2;
    public float ascendingThreshold = 2;
}

public class IsHoveringCondition : Condition
{
    private Character _character;
    private IsHoveringConditionSO _originSO => (IsHoveringConditionSO)base.OriginSO; // The SO this Condition spawned from
    public override void Awake(StateMachine stateMachine)
    {
        _character = stateMachine.GetComponent<Character>();
    }

    protected override bool Statement()
    {
        return _character.movementVector.y <= _originSO.ascendingThreshold && _character.movementVector.y >= _originSO.descendingThreashold;
    }
}
