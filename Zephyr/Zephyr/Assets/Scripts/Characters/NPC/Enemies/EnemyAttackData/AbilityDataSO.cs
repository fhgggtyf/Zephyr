using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "AbilityDat")]
public class AbilityDataSO : ScriptableObject
{
    public DamageType type;

    public AbilityDataSO()
    {
        type = DamageType.True;
    }
}
