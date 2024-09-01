using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] protected StatsConfigSO statsConfig = default;
    [SerializeField] protected IngameStatsSO currentStatsSO = default;

    public StatsConfigSO StatsConfig { get => statsConfig; set => statsConfig = value; }
    public IngameStatsSO CurrentStatsSO { get => currentStatsSO; set => currentStatsSO = value; }

    protected void InitializeStats()
    {
        CurrentStatsSO.SetMaxHealth(StatsConfig.InitialHealth);
        CurrentStatsSO.SetCurrentHealth(StatsConfig.InitialHealth);
        CurrentStatsSO.SetCurrentArmor(StatsConfig.InitialArmor);
        CurrentStatsSO.SetCurrentMR(StatsConfig.InitialMagicResist);
        CurrentStatsSO.SetCurrentMaxJumps(StatsConfig.InitialJumpCount);
        CurrentStatsSO.SetCurrentAttack(StatsConfig.InitialAttack);
        CurrentStatsSO.SetCurrentAP(StatsConfig.InitialAbilityPower);
        CurrentStatsSO.SetCurrentCD(StatsConfig.InitialCooldown);
        CurrentStatsSO.SetCurrentAttackSpeed(StatsConfig.InitialBaseAttackSpeed);
        CurrentStatsSO.SetCurrentMana(StatsConfig.InitialMana);
        CurrentStatsSO.SetCurrentLuck(StatsConfig.InitialLuck);
        CurrentStatsSO.SetCurrentStamina(StatsConfig.InitialStamina);
        CurrentStatsSO.SetMaxStamina(StatsConfig.InitialStamina);
        CurrentStatsSO.SetCurrentTenacity(StatsConfig.InitialTenacity);
        CurrentStatsSO.SetCurrentCritChance(StatsConfig.InitialCritChance);
        CurrentStatsSO.SetCurrentCritDmg(StatsConfig.InitialCritDamage);
    }

    public int GetCurrentTenacity()
    {
        return currentStatsSO.CurrentTenacity;
    }
}
