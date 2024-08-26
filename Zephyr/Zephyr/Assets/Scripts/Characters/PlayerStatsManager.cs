using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private IEnumerator RestoreStaminaCoroutine;

    [SerializeField] private IngameStatsSO _protagonistStats = default;
    [SerializeField] private StatsConfigSO _StatsConfig = default;

    [SerializeField] private VoidEventChannelSO _updateStaminaUI = default;

    [SerializeField] private bool _canRestoreStamina = true;

    public bool CanRestoreStamina { get => _canRestoreStamina; set => _canRestoreStamina = value; }

    public bool updatedFlag = true;

    private void OnEnable()
    {
        InitializeStats();
        RestoreStaminaCoroutine = RestoreStamina();
        StartCoroutine(RestoreStaminaCoroutine);
    }
    IEnumerator RestoreStamina()
    {

        while (true)
        {
            // �ȴ�ֱ�����Իָ�stamina  
            while (!CanRestoreStamina)
            {
                yield return null; // ��ͣЭ�̵�������ʱ�䣬����ʹ��yield return new WaitForSeconds(smallDelay)�����С�ӳ�  
            }
            updatedFlag = false;
            for (int i = 0; i < 15; i++)
            {
                // ��ͣ����  
                yield return new WaitForSeconds(0.1f);
                if (updatedFlag)
                {
                    break;
                }
            }

            // ��ʼ�ָ�stamina��ֱ��������������  
            while (CanRestoreStamina && !updatedFlag)
            {
                // ƽ������stamina���߼�  
                // ����RestoreStamina�����ᴦ��stamina�����ӣ����Ҳ��ᳬ�����ֵ  
                float amountToRestore = CalculateAmountToRestore(); // ����Ҫʵ���������������ÿ��Ҫ�ָ���stamina��  
                _protagonistStats.RestoreStamina(amountToRestore);
                _updateStaminaUI.RaiseEvent(); // ����UI  

                // �������ǲ�ϣ��ÿ�����Ӷ��ȴ������Ǹ������ӵ����������ȴ�ʱ�䣨����Ϊ�˼����������ֱ�Ӽ�����  
                // ���������Ҫƽ���Ķ���Ч�����������Ҫ���������һЩ�ӳ�  
                // yield return new WaitForSeconds(smallDelay); // smallDelay��һ��С���ӳ�ֵ������0.1f  

                // ����Ƿ���Ȼ���Իָ�stamina�����������������ڲ�ѭ���в��Ǳ���ģ���������ⲿ�������ܸı�������Ӧ�ñ����  
                if (!CanRestoreStamina)
                    break;

                // ע�⣺���RestoreStamina�����ڲ��Ѿ�������stamina�����ֵ���ƣ�������ļ����ܲ��Ǳ����  
                // �����δ�������Ӧ������������߼�����ֹ�������ֵ  

                // һ��С�ӳ���ģ��ƽ�����ӣ���ѡ��  
                yield return new WaitForSeconds(0.1f); // ������Ҫ�������ֵ  
            }

            // ��canRestoreStamina��Ϊ��ʱ��Э�̻�ص����ѭ���Ŀ�ʼ�����ȴ�ֱ�����ٴα�Ϊ��  
        }

    }

    private float CalculateAmountToRestore()
    {
        // ����ֻ��һ��ʾ�����������Ҫ���������Ϸ�߼�������������㷽��  
        float maxStamina = _protagonistStats.MaxStamina;
        float amount = Mathf.Max(maxStamina * 0.02f, 1f); // ÿ�����ָ�1�㣬���߻ָ������ֵ  
        return amount;
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void InitializeStats()
    {
        _protagonistStats.SetMaxHealth(_StatsConfig.InitialHealth);
        _protagonistStats.SetCurrentHealth(_StatsConfig.InitialHealth);
        _protagonistStats.SetCurrentArmor(_StatsConfig.InitialArmor);
        _protagonistStats.SetCurrentMR(_StatsConfig.InitialMagicResist);
        _protagonistStats.SetCurrentMaxJumps(_StatsConfig.InitialJumpCount);
        _protagonistStats.SetCurrentAttack(_StatsConfig.InitialAttack);
        _protagonistStats.SetCurrentAP(_StatsConfig.InitialAbilityPower);
        _protagonistStats.SetCurrentCD(_StatsConfig.InitialCooldown);
        _protagonistStats.SetCurrentAttackSpeed(_StatsConfig.InitialBaseAttackSpeed);
        _protagonistStats.SetCurrentMana(_StatsConfig.InitialMana);
        _protagonistStats.SetCurrentLuck(_StatsConfig.InitialLuck);
        _protagonistStats.SetCurrentStamina(_StatsConfig.InitialStamina);
        _protagonistStats.SetMaxStamina(_StatsConfig.InitialStamina);
        _protagonistStats.SetCurrentTenacity(_StatsConfig.InitialTenacity);
        _protagonistStats.SetCurrentCritChance(_StatsConfig.InitialCritChance);
        _protagonistStats.SetCurrentCritDmg(_StatsConfig.InitialCritDamage);
    }

    public int GetMaxJumps()
    {
        return _protagonistStats.CurrentMaxJumps;
    }

    public void SpendStamina(float spent)
    {
        _protagonistStats.SpendStamina(spent);
        _updateStaminaUI.RaiseEvent();
    }

    public void ReturnStamina(int returned)
    {
        _protagonistStats.RestoreStamina(returned);
        _updateStaminaUI.RaiseEvent();
    }

    public float GetCurrentStamina()
    {
        return _protagonistStats.CurrentStamina;
    }

    public float GetCurrentAttackSpeed()
    {
        return _protagonistStats.CurrentAttackSpeed;
    }

    public int GetCurrentTenacity()
    {
        return _protagonistStats.CurrentTenacity;
    }
}

public enum StatTypes
{
    MaxHealth,
    CurrentHealth,
    Armor,
    MagicResist,
    Attack,
    MagicPower,
    ArmorIgnor,
    MRIgnore,
    Stamina,
    Tenacity,
    Luck

}
