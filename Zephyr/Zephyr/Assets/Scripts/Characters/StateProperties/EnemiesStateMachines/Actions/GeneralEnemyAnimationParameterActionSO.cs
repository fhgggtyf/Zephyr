using UnityEngine;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;
using Moment = Zephyr.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "GeneralEnemyAnimatorParameterAction", menuName = "State Machines/Actions/Enemies/Set Animator Parameter", order = 0)]
public class GeneralEnemyAnimationParameterActionSO : StateActionSO
{
    public string animName = default;
    public Moment whenToRun = default;
    public float playSpeed = 1;

    protected override StateAction CreateAction() => new GeneralEnemyAnimationParameterAction(animName, playSpeed);

}
public class GeneralEnemyAnimationParameterAction : StateAction
{
    private AnimationManager _animManager;
    private GeneralEnemyAnimationParameterActionSO _originSO => (GeneralEnemyAnimationParameterActionSO)base.OriginSO; // The SO this StateAction spawned from
    private string _animName;
    private float _playSpeed;

    public GeneralEnemyAnimationParameterAction(string param, float playSpeedParam)
    {
        _animName = param;
        _playSpeed = playSpeedParam;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _animManager = stateMachine.GetComponent<AnimationManager>();
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
        _animManager.ChangeAnimState(_animName, _playSpeed);
    }

    public override void OnUpdate() { }

}


