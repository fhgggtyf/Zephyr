using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private IngameStatsSO _protagonistStats = default;
    [SerializeField] private StatsConfigSO _StatsConfig = default;

    private void OnEnable()
    {
        InitializeStats();
    }

    private void OnDestroy()
    {
        
    }

    private void InitializeStats()
    {
        _protagonistStats.SetMaxHealth(_StatsConfig.InitialHealth);
        _protagonistStats.SetCurrentHealth(_StatsConfig.InitialHealth);

    }
}
