using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyDataSO : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; set; }
    [field: SerializeField] public string Name { get; private set; }
}
