using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Enemies/WallDetected")]
public class GeneralEnemyDetectWallConditionSO : StateConditionSO<GeneralEnemyDetectWallCondition>
{
}                     

public class GeneralEnemyDetectWallCondition : Condition
{
	private NonPlayerCharacter _npcScript;
	private CollisionSenses _collisionSenses;

	public override void Awake(StateMachine stateMachine)
	{
		_npcScript = stateMachine.GetComponent<NonPlayerCharacter>();
		_collisionSenses = _npcScript.Core.GetCoreComponent(ref _collisionSenses);
	}

	protected override bool Statement()
	{
		return _collisionSenses.WallFront;
	}
}
