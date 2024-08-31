using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Enemies/Can Interupt Attack")]
public class GeneralEnemyCanBeInteruptedConditionSO : StateConditionSO<GeneralEnemyCanBeInteruptedCondition>
{
    public AbilityDataSO abilityData;
}
public class GeneralEnemyCanBeInteruptedCondition : Condition
{
    private GeneralEnemyCanBeInteruptedConditionSO _originSO => (GeneralEnemyCanBeInteruptedConditionSO)base.OriginSO; // The SO this Condition spawned from

    protected override bool Statement()
    {
        return _originSO.abilityData.canBeInterupted;
    }
}
