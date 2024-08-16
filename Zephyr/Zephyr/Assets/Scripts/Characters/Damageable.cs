using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
	[Header("Health")]
	[SerializeField] private StatsManager _statsManager;
	[SerializeField] private StatsConfigSO _healthConfigSO;
	[SerializeField] private IngameStatsSO _currentStatsSO;

	//[Header("Combat")]
	//[SerializeField] private GetHitEffectConfigSO _getHitEffectSO;
	//[SerializeField] private Renderer _mainMeshRenderer;
	//[SerializeField] private DroppableRewardConfigSO _droppableRewardSO;

	[Header("Broadcasting On")]
	[SerializeField] private VoidEventChannelSO _updateHealthUI = default;
	[SerializeField] private VoidEventChannelSO _deathEvent = default;

	[Header("Listening To")]
	[SerializeField] private IntEventChannelSO _restoreHealth = default; //Getting cured when eating food

	//public DroppableRewardConfigSO DroppableRewardConfig => _droppableRewardSO;

	//Flags that the StateMachine uses for Conditions to move between states
	public bool GetHit { get; set; }
	public bool IsDead { get; set; }

	//public GetHitEffectConfigSO GetHitEffectConfig => _getHitEffectSO;
	//public Renderer MainMeshRenderer => _mainMeshRenderer; //used to apply the hit flash effect

	public event UnityAction OnDie;

	private void Awake()
	{

		if (_updateHealthUI != null)
			_updateHealthUI.RaiseEvent();
	}

	private void OnEnable()
    {
        if (_statsManager != null)
        {
            _healthConfigSO = _statsManager._StatsConfig;
            _currentStatsSO = _statsManager.currentStatsSO;
        }


        if (_restoreHealth != null)
            _restoreHealth.OnEventRaised += Cure;
	}

	private void OnDisable()
	{
		if (_restoreHealth != null)
			_restoreHealth.OnEventRaised -= Cure;
	}

	public void ReceiveAnAttack(DamageData data)
	{
		if (IsDead)
			return;

		float damage = CalculateActualDamage(data);

		_currentStatsSO.InflictDamage((int)damage);

		if (_updateHealthUI != null)
			_updateHealthUI.RaiseEvent();

		GetHit = true;

		if (_currentStatsSO.CurrentHealth <= 0)
		{
			IsDead = true;

			if (OnDie != null)
				OnDie.Invoke();

			if (_deathEvent != null)
				_deathEvent.RaiseEvent();

			_currentStatsSO.SetCurrentHealth(_healthConfigSO.InitialHealth);
		}
	}

	private float CalculateActualDamage(DamageData data)
    {
        switch (data.AbilityParam.type)
        {
			case DamageType.Physical:
				int equivArmor = _currentStatsSO.CurrentArmor - data.ArmorIgnore;
				return data.Amount * (1f - (equivArmor / (100f + equivArmor)));
			case DamageType.Magical:
				int equivMR = _currentStatsSO.CurrentMR - data.MrIgnore;
				return data.Amount * (1f - (equivMR / (100f + equivMR)));
			case DamageType.True:
				return data.Amount;
		}
		return 0;
    }

    public void Kill()
    {
        ReceiveAnAttack(new DamageData(_currentStatsSO.CurrentHealth, 0, 0, new AbilityDataSO(), EnemyType.None, null));
    }

    /// <summary>
    /// Called by the StateMachine action ResetHealthSO. Used to revive the Rock critters.
    /// </summary>
    public void Revive()
	{
		_currentStatsSO.SetCurrentHealth(_healthConfigSO.InitialHealth);

		if (_updateHealthUI != null)
			_updateHealthUI.RaiseEvent();

		IsDead = false;
	}

	/// <summary>
	/// Used for cure events, like eating food. Triggered by an IntEventChannelSO.
	/// </summary>
	private void Cure(int healthToAdd)
	{
		if (IsDead)
			return;

		_currentStatsSO.RestoreHealth(healthToAdd);

		if (_updateHealthUI != null)
			_updateHealthUI.RaiseEvent();
	}
}
