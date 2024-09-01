using UnityEngine;
using TMPro;

public class UIManaBarManager : MonoBehaviour
{
	[SerializeField] private IngameStatsSO _protagonistStats = default; //the ManaBar is watching this object, which is the Mana of the player
	[SerializeField] private StatsConfigSO _StatsConfig = default;
	[SerializeField] private TMP_Text _ManaText = default;

	[Header("Listening to")]
	[SerializeField] private VoidEventChannelSO _UIUpdateNeeded = default; //The player's Damageable issues this

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
		//int heartValue = _protagonistMana.MaxMana / _heartImages.Length;
		//int filledHeartCount = Mathf.FloorToInt((float)_protagonistMana.CurrentMana / heartValue);

		//for (int i = 0; i < _heartImages.Length; i++)
		//{
		//	float heartPercent = 0;

		//	if (i < filledHeartCount)
		//	{
		//		heartPercent = 1;
		//	}
		//	else if (i == filledHeartCount)
		//	{
		//		heartPercent = ((float)_protagonistMana.CurrentMana - (float)filledHeartCount * (float)heartValue) / (float)heartValue;
		//	}
		//	else
		//	{
		//		heartPercent = 0;
		//	}
		//	_heartImages[i].SetImage(heartPercent);
		//}

		_ManaText.SetText(Mathf.FloorToInt((float)_protagonistStats.CurrentMana).ToString());
	}
}
