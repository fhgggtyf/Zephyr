using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : StatsManager
{
    private IEnumerator RestoreStaminaCoroutine;

    [SerializeField] private VoidEventChannelSO _updateStaminaUI = default;
    [SerializeField] private SOEventChannelSO _statsSendingChannel = default;

    [SerializeField] private bool _canRestoreStamina = true;

    public bool CanRestoreStamina { get => _canRestoreStamina; set => _canRestoreStamina = value; }

    public bool updatedFlag = true;

    private void OnEnable()
    {
        _statsSendingChannel.OnEventRaised += InitializeStats;
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
                currentStatsSO.RestoreStamina(amountToRestore);
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

    protected void InitializeStats(ScriptableObject statsReceived)
    {
        StatsConfigSO stats = (StatsConfigSO)statsReceived;

        CurrentStatsSO.SetMaxHealth(stats.InitialHealth);
        CurrentStatsSO.SetCurrentHealth(stats.InitialHealth);
        CurrentStatsSO.SetCurrentArmor(stats.InitialArmor);
        CurrentStatsSO.SetCurrentMR(stats.InitialMagicResist);
        CurrentStatsSO.SetCurrentMaxJumps(stats.InitialJumpCount);
        CurrentStatsSO.SetCurrentAttack(stats.InitialAttack);
        CurrentStatsSO.SetCurrentAP(stats.InitialAbilityPower);
        CurrentStatsSO.SetCurrentCD(stats.InitialCooldown);
        CurrentStatsSO.SetCurrentAttackSpeed(stats.InitialBaseAttackSpeed);
        CurrentStatsSO.SetCurrentMana(stats.InitialMana);
        CurrentStatsSO.SetCurrentLuck(stats.InitialLuck);
        CurrentStatsSO.SetCurrentStamina(stats.InitialStamina);
        CurrentStatsSO.SetMaxStamina(stats.InitialStamina);
        CurrentStatsSO.SetCurrentTenacity(stats.InitialTenacity);
        CurrentStatsSO.SetCurrentCritChance(stats.InitialCritChance);
        CurrentStatsSO.SetCurrentCritDmg(stats.InitialCritDamage);
    }

    private float CalculateAmountToRestore()
    {
        // ����ֻ��һ��ʾ�����������Ҫ���������Ϸ�߼�������������㷽��  
        float maxStamina = currentStatsSO.MaxStamina;
        float amount = Mathf.Max(maxStamina * 0.02f, 1f); // ÿ�����ָ�1�㣬���߻ָ������ֵ  
        return amount;
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public int GetMaxJumps()
    {
        return currentStatsSO.CurrentMaxJumps;
    }

    public void SpendStamina(float spent)
    {
        currentStatsSO.SpendStamina(spent);
        _updateStaminaUI.RaiseEvent();
    }

    public void ReturnStamina(int returned)
    {
        currentStatsSO.RestoreStamina(returned);
        _updateStaminaUI.RaiseEvent();
    }

    public float GetCurrentStamina()
    {
        return currentStatsSO.CurrentStamina;
    }

    public float GetCurrentAttackSpeed()
    {
        return currentStatsSO.CurrentAttackSpeed;
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
