using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class UIManaBarManager : MonoBehaviour
{
	[SerializeField] private IngameStatsSO _protagonistStats = default; //the ManaBar is watching this object, which is the Mana of the player
	[SerializeField] private StatsConfigSO _StatsConfig = default;
	[SerializeField] private TMP_Text _ManaText = default;
	[SerializeField] private Image _manaBarImage;
	[SerializeField] private float _timeToDrop;

	[Header("Listening to")]
	[SerializeField] private VoidEventChannelSO _UIUpdateNeeded = default; //The player's Damageable issues this

	private float _manaPercentage;

	private void OnEnable()
	{
		_UIUpdateNeeded.OnEventRaised += UpdateMana;

		InitializeManaBar();
	}

	private void OnDestroy()
	{
		_UIUpdateNeeded.OnEventRaised -= UpdateMana;
	}

	private void InitializeManaBar()
	{
		UpdateMana();
	}

	private void UpdateMana()
	{
		_manaPercentage = (float)_protagonistStats.CurrentMana / _StatsConfig.InitialMana;
		StartCoroutine(SmoothManaBar());

		_ManaText.SetText(Mathf.FloorToInt((float)_protagonistStats.CurrentMana).ToString());
	}

	private IEnumerator SmoothManaBar()
	{
		float preChangeValue = _manaBarImage.fillAmount;
		float elapsedTime = 0f;

		while (elapsedTime < _timeToDrop)
		{
			elapsedTime += Time.deltaTime;
			_manaBarImage.fillAmount = Mathf.Lerp(preChangeValue, _manaPercentage, elapsedTime / _timeToDrop);
			yield return null;  // Wait for the next frame
		}

		_manaBarImage.fillAmount = _manaPercentage;  // Ensure the final value is set correctly
	}
}
