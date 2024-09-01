using UnityEngine;
using TMPro;

public class UIStaminaBarManager : MonoBehaviour
{
	[SerializeField] private IngameStatsSO _protagonistStats = default; //the StaminaBar is watching this object, which is the Stamina of the player
	[SerializeField] private StatsConfigSO _StatsConfig = default;
	[SerializeField] private TMP_Text _StaminaText = default;

	[Header("Listening to")]
	[SerializeField] private VoidEventChannelSO _UIUpdateNeeded = default; //The player's Damageable issues this

	private void OnEnable()
	{
		_UIUpdateNeeded.OnEventRaised += UpdateStamina;

		InitializeStaminaBar();
	}

	private void OnDestroy()
	{
		_UIUpdateNeeded.OnEventRaised -= UpdateStamina;
	}

	private void InitializeStaminaBar()
	{
		UpdateStamina();
	}

	private void UpdateStamina()
	{
		//int heartValue = _protagonistStamina.MaxStamina / _heartImages.Length;
		//int filledHeartCount = Mathf.FloorToInt((float)_protagonistStamina.CurrentStamina / heartValue);

		//for (int i = 0; i < _heartImages.Length; i++)
		//{
		//	float heartPercent = 0;

		//	if (i < filledHeartCount)
		//	{
		//		heartPercent = 1;
		//	}
		//	else if (i == filledHeartCount)
		//	{
		//		heartPercent = ((float)_protagonistStamina.CurrentStamina - (float)filledHeartCount * (float)heartValue) / (float)heartValue;
		//	}
		//	else
		//	{
		//		heartPercent = 0;
		//	}
		//	_heartImages[i].SetImage(heartPercent);
		//}

		_StaminaText.SetText(Mathf.FloorToInt(_protagonistStats.CurrentStamina).ToString());
	}
}
