using UnityEngine;
using TMPro;

public class CharacterHealthBarManager : MonoBehaviour
{
    //[SerializeField] private IngameStatsSO _characterStats = default; //the HealthBar is watching this object, which is the health of the player
    //[SerializeField] private StatsConfigSO _StatsConfig = default;
    [SerializeField] private NonPlayerStatsManager _statsManager = default;
    [SerializeField] private Damageable _damageable = default;
    [SerializeField] private TMP_Text _healthText = default;

    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _barUpdateNeeded; //The player's Damageable issues this

    private void OnEnable()
    {
        _barUpdateNeeded = _damageable.updateHealthBar;

        _barUpdateNeeded.OnEventRaised += UpdateHealth;

        InitializeHealthBar();
    }

    private void OnDestroy()
    {
        _barUpdateNeeded.OnEventRaised -= UpdateHealth;
    }

    private void InitializeHealthBar()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        //int heartValue = _protagonistHealth.MaxHealth / _heartImages.Length;
        //int filledHeartCount = Mathf.FloorToInt((float)_protagonistHealth.CurrentHealth / heartValue);

        //for (int i = 0; i < _heartImages.Length; i++)
        //{
        //	float heartPercent = 0;

        //	if (i < filledHeartCount)
        //	{
        //		heartPercent = 1;
        //	}
        //	else if (i == filledHeartCount)
        //	{
        //		heartPercent = ((float)_protagonistHealth.CurrentHealth - (float)filledHeartCount * (float)heartValue) / (float)heartValue;
        //	}
        //	else
        //	{
        //		heartPercent = 0;
        //	}
        //	_heartImages[i].SetImage(heartPercent);
        //}

        _healthText.SetText(Mathf.FloorToInt((float)_statsManager.currentStatsSO.CurrentHealth).ToString());
    }
}
