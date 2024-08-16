using UnityEngine;
using UnityEngine.Events;
using Zephyr.StateMachine;
using Zephyr.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AlertIconAction", menuName = "State Machines/Actions/Enemies/Alert icon")]
public class GeneralEnemyAlertIconActionSO : StateActionSO<GeneralEnemyAlertIconAction>
{
    public AlertIconTypes iconType;
}

public class GeneralEnemyAlertIconAction : StateAction
{
    private NonPlayerCharacter _npc;

    private GeneralEnemyAlertIconActionSO _originSO => (GeneralEnemyAlertIconActionSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _npc = stateMachine.GetComponent<NonPlayerCharacter>();
    }

    public override void OnStateEnter()
    {
        DoAction(true);
    }

    public override void OnStateExit()
    {
        DoAction(false);
    }

    public override void OnUpdate()
    {
    }

    private void DoAction(bool flag)
    {
        switch (_originSO.iconType)
        {
            case AlertIconTypes.Alert:
                _npc.alertSymbol.SetActive(flag);
                break;
            case AlertIconTypes.LookForPlayer:
                _npc.lookForPlayerSymbol.SetActive(flag);
                break;
        }
    }
}

public enum AlertIconTypes
{
    Alert,
    LookForPlayer
}