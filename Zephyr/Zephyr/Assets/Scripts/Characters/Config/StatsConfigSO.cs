using UnityEngine;

[CreateAssetMenu(fileName = "StatsConfig", menuName = "EntityConfig/Stats Config")]
public class StatsConfigSO : ScriptableObject
{
	[SerializeField] private int _initialBaseHealth;
	[SerializeField] private int _initialBaseArmor;
	[SerializeField] private int _initialBaseMagicResist;
	[SerializeField] private int _initialBaseAttack;
	[SerializeField] private int _initialBaseAbilityPower;
	[SerializeField] private int _initialBaseCooldown;
	[SerializeField] private int _initialBaseAttackSpeed;
	[SerializeField] private int _initialBaseMana;
	[SerializeField] private int _initialBaseTenacity;
	[SerializeField] private int _initialBaseStamina;
	[SerializeField] private int _initialBaseLuck;

	[SerializeField] private float _initialCritChance;
	[SerializeField] private float _initialCritDamage;
	[SerializeField] private int _initialJumpCount;

	public int InitialHealth { get => _initialBaseHealth; set => _initialBaseHealth = value; }
    public int InitialArmor { get => _initialBaseArmor; set => _initialBaseArmor = value; }
    public int InitialMagicResist { get => _initialBaseMagicResist; set => _initialBaseMagicResist = value; }
    public int InitialAttack { get => _initialBaseAttack; set => _initialBaseAttack = value; }
    public int InitialAbilityPower { get => _initialBaseAbilityPower; set => _initialBaseAbilityPower = value; }
    public int InitialCooldown { get => _initialBaseCooldown; set => _initialBaseCooldown = value; }
    public int InitialMana { get => _initialBaseMana; set => _initialBaseMana = value; }
    public int InitialTenacity { get => _initialBaseTenacity; set => _initialBaseTenacity = value; }
    public int InitialStamina { get => _initialBaseStamina; set => _initialBaseStamina = value; }
    public int InitialLuck { get => _initialBaseLuck; set => _initialBaseLuck = value; }
    public float InitialCritChance { get => _initialCritChance; set => _initialCritChance = value; }
    public float InitialCritDamage { get => _initialCritDamage; set => _initialCritDamage = value; }
    public int InitialJumpCount { get => _initialJumpCount; set => _initialJumpCount = value; }
    public int InitialBaseAttackSpeed { get => _initialBaseAttackSpeed; set => _initialBaseAttackSpeed = value; }
}
