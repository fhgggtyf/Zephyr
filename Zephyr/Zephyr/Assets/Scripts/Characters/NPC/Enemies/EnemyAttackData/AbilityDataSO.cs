using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "AbilityData")]
public class AbilityDataSO : ScriptableObject
{
    public DamageType type;

    public AbilityDataSO()
    {
        type = DamageType.True;
    }
}
