using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class UIStaminaBarManager : MonoBehaviour
{
	[SerializeField] private IngameStatsSO _protagonistStats = default; //the StaminaBar is watching this object, which is the Stamina of the player
	[SerializeField] private StatsConfigSO _StatsConfig = default;
	[SerializeField] private TMP_Text _StaminaText = default;
	[SerializeField] private Image _staminaBarImage;
	[SerializeField] private float _timeToDrop;

	[Header("Listening to")]
	[SerializeField] private VoidEventChannelSO _UIUpdateNeeded = default; //The player's Damageable issues this

	private float _staminaPercentage;

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
		_staminaPercentage = (float)_protagonistStats.CurrentStamina / _StatsConfig.InitialStamina;
		StartCoroutine(SmoothStaminaBar());

		_StaminaText.SetText(Mathf.FloorToInt(_protagonistStats.CurrentStamina).ToString());
	}

	private IEnumerator SmoothStaminaBar()
	{
		float preChangeValue = _staminaBarImage.fillAmount;
		float elapsedTime = 0f;

		while (elapsedTime < _timeToDrop)
		{
			elapsedTime += Time.deltaTime;
			_staminaBarImage.fillAmount = Mathf.Lerp(preChangeValue, _staminaPercentage, elapsedTime / _timeToDrop);
			yield return null;  // Wait for the next frame
		}

		_staminaBarImage.fillAmount = _staminaPercentage;  // Ensure the final value is set correctly
	}
}
