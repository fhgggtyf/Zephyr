using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerStatsManager : MonoBehaviour
{
    [SerializeField] public StatsConfigSO statsConfig = default;
    [SerializeField] public IngameStatsSO currentStatsSO;

    public void Awake()
    {
        //If the HealthSO hasn't been provided in the Inspector (as it's the case for the player),
        //we create a new SO unique to this instance of the component. This is typical for enemies.
        if (currentStatsSO == null)
        {
            Debug.Log(2);
            currentStatsSO = ScriptableObject.CreateInstance<IngameStatsSO>();
            InitializeStats();
        }
    }

    private void InitializeStats()
    {
        currentStatsSO.SetMaxHealth(statsConfig.InitialHealth);
        currentStatsSO.SetCurrentHealth(statsConfig.InitialHealth);
        currentStatsSO.SetCurrentArmor(statsConfig.InitialArmor);
        currentStatsSO.SetCurrentMR(statsConfig.InitialMagicResist);
        currentStatsSO.SetCurrentMaxJumps(statsConfig.InitialJumpCount);
        currentStatsSO.SetCurrentAttack(statsConfig.InitialAttack);
        currentStatsSO.SetCurrentAP(statsConfig.InitialAbilityPower);
        currentStatsSO.SetCurrentCD(statsConfig.InitialCooldown);
        currentStatsSO.SetCurrentAttackSpeed(statsConfig.InitialBaseAttackSpeed);
        currentStatsSO.SetCurrentMana(statsConfig.InitialMana);
        currentStatsSO.SetCurrentLuck(statsConfig.InitialLuck);
        currentStatsSO.SetCurrentStamina(statsConfig.InitialStamina);
        currentStatsSO.SetMaxStamina(statsConfig.InitialStamina);
        currentStatsSO.SetCurrentTenacity(statsConfig.InitialTenacity);
        currentStatsSO.SetCurrentCritChance(statsConfig.InitialCritChance);
        currentStatsSO.SetCurrentCritDmg(statsConfig.InitialCritDamage);
    }
}
