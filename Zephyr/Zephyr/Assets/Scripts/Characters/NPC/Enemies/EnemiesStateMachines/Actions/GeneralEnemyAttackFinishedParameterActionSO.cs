using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "GeneralEnemyAttackFinishedParameterAction", menuName = "State Machines/Actions/Enemies/Set attackFinished Parameter", order = 0)]
public class GeneralEnemyAttackFinishedParameterActionSO : StateActionSO
{
    public bool flag = default;
    public Moment whenToRun = default;

    protected override StateAction CreateAction() => new GeneralEnemyAttackFinishedParameterAction(flag);

}
public class GeneralEnemyAttackFinishedParameterAction : StateAction
{
    private NonPlayerCharacter _npc;
    private GeneralEnemyAttackFinishedParameterActionSO _originSO => (GeneralEnemyAttackFinishedParameterActionSO)base.OriginSO; // The SO this StateAction spawned from
    private bool _flag;

    public GeneralEnemyAttackFinishedParameterAction(bool param)
    {
        _flag = param;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _npc = stateMachine.GetComponent<NonPlayerCharacter>();
    }

    public override void OnStateEnter()
    {
        if (_originSO.whenToRun == SpecificMoment.OnStateEnter)
            SetParameter();
    }

    public override void OnStateExit()
    {
        if (_originSO.whenToRun == SpecificMoment.OnStateExit)
            SetParameter();
    }

    private void SetParameter()
    {
        _npc.attackFinished = _flag;
    }

    public override void OnUpdate() { }

}


