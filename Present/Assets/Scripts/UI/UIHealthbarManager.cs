using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class UIHealthBarManager : MonoBehaviour
{
	[SerializeField] private IngameStatsSO _protagonistStats = default; //the HealthBar is watching this object, which is the health of the player
	[SerializeField] private StatsConfigSO _StatsConfig = default;
	[SerializeField] private TMP_Text _healthText = default;
	[SerializeField] private Image _healthBarImage;
	[SerializeField] private float _timeToDrop;

	[Header("Listening to")]
	[SerializeField] private VoidEventChannelSO _UIUpdateNeeded = default; //The player's Damageable issues this

	private float _healthPercentage;

	private void OnEnable()
	{
		_UIUpdateNeeded.OnEventRaised += UpdateHealth;

		InitializeHealthBar();
	}

	private void OnDestroy()
	{
		_UIUpdateNeeded.OnEventRaised -= UpdateHealth;
	}

	private void InitializeHealthBar()
	{
		UpdateHealth();
	}

	private void UpdateHealth()
	{
		_healthPercentage = (float)_protagonistStats.CurrentHealth / _StatsConfig.InitialHealth;
		Debug.Log(_healthPercentage);
		StartCoroutine(SmoothHealthBar());

		_healthText.SetText(Mathf.FloorToInt((float)_protagonistStats.CurrentHealth).ToString());
	}


	private IEnumerator SmoothHealthBar()
	{
		float preChangeValue = _healthBarImage.fillAmount;
		float elapsedTime = 0f;

		while (elapsedTime < _timeToDrop)
		{
			elapsedTime += Time.deltaTime;
			_healthBarImage.fillAmount = Mathf.Lerp(preChangeValue, _healthPercentage, elapsedTime / _timeToDrop);
			yield return null;  // Wait for the next frame
		}

		_healthBarImage.fillAmount = _healthPercentage;  // Ensure the final value is set correctly
	}
}
