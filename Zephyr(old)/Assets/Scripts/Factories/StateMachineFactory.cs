using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineFactory<TStateMachine, TCharacter, TData, TCore> : ScriptableObject
{
    public abstract TStateMachine CreateStateMachine(TCharacter character, TData data, TCore core);
}
