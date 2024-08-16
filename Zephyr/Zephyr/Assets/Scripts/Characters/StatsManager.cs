using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
	[SerializeField] public StatsConfigSO _StatsConfig = default;
	[SerializeField] public IngameStatsSO currentStatsSO;

    public void Awake()
    {
		//If the HealthSO hasn't been provided in the Inspector (as it's the case for the player),
		//we create a new SO unique to this instance of the component. This is typical for enemies.
		if (currentStatsSO == null)
		{
			currentStatsSO = ScriptableObject.CreateInstance<IngameStatsSO>();
            InitializeStats();
		}
	}

    private void InitializeStats()
    {
        currentStatsSO.SetMaxHealth(_StatsConfig.InitialHealth);
        currentStatsSO.SetCurrentHealth(_StatsConfig.InitialHealth);
        currentStatsSO.SetCurrentArmor(_StatsConfig.InitialArmor);
        currentStatsSO.SetCurrentMR(_StatsConfig.InitialMagicResist);
        currentStatsSO.SetCurrentMaxJumps(_StatsConfig.InitialJumpCount);
        currentStatsSO.SetCurrentAttack(_StatsConfig.InitialAttack);
        currentStatsSO.SetCurrentAP(_StatsConfig.InitialAbilityPower);
        currentStatsSO.SetCurrentCD(_StatsConfig.InitialCooldown);
        currentStatsSO.SetCurrentAttackSpeed(_StatsConfig.InitialBaseAttackSpeed);
        currentStatsSO.SetCurrentMana(_StatsConfig.InitialMana);
        currentStatsSO.SetCurrentLuck(_StatsConfig.InitialLuck);
        currentStatsSO.SetCurrentStamina(_StatsConfig.InitialStamina);
        currentStatsSO.SetMaxStamina(_StatsConfig.InitialStamina);
        currentStatsSO.SetCurrentTenacity(_StatsConfig.InitialTenacity);
        currentStatsSO.SetCurrentCritChance(_StatsConfig.InitialCritChance);
        currentStatsSO.SetCurrentCritDmg(_StatsConfig.InitialCritDamage);
    }
}
