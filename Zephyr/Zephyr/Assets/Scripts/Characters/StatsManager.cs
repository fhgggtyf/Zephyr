using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private IEnumerator RestoreStaminaCoroutine;

    [SerializeField] private IngameStatsSO _protagonistStats = default;
    [SerializeField] private StatsConfigSO _StatsConfig = default;

    [SerializeField] private VoidEventChannelSO _updateStaminaUI = default;

    private bool _canRestoreStamina = true;

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
            // 等待直到可以恢复stamina  
            while (!CanRestoreStamina)
            {
                yield return null; // 暂停协程但不消耗时间，或者使用yield return new WaitForSeconds(smallDelay)来添加小延迟  
            }
            updatedFlag = false;
            // 暂停两秒  
            yield return new WaitForSeconds(1.5f);

            // 开始恢复stamina，直到条件不再满足  
            while (CanRestoreStamina && !updatedFlag)
            {
                // 平滑增加stamina的逻辑  
                // 假设RestoreStamina方法会处理stamina的增加，并且不会超过最大值  
                float amountToRestore = CalculateAmountToRestore(); // 你需要实现这个方法来计算每次要恢复的stamina量  
                _protagonistStats.RestoreStamina(amountToRestore);
                _updateStaminaUI.RaiseEvent(); // 更新UI  

                // 假设我们不希望每次增加都等待，而是根据增加的量来决定等待时间（这里为了简单起见，我们直接继续）  
                // 但如果你想要平滑的动画效果，你可能需要在这里添加一些延迟  
                // yield return new WaitForSeconds(smallDelay); // smallDelay是一个小的延迟值，比如0.1f  

                // 检查是否仍然可以恢复stamina（理论上这个检查在内部循环中不是必需的，但如果有外部条件可能改变它，则应该保留）  
                if (!CanRestoreStamina)
                    break;

                // 注意：如果RestoreStamina方法内部已经处理了stamina的最大值限制，则下面的检查可能不是必需的  
                // 但如果未处理，你应该在这里添加逻辑来防止超过最大值  

                // 一个小延迟来模拟平滑增加（可选）  
                yield return new WaitForSeconds(0.1f); // 根据需要调整这个值  
            }

            // 当canRestoreStamina变为假时，协程会回到外层循环的开始，并等待直到它再次变为真  
        }

    }

    private float CalculateAmountToRestore()
    {
        // 这里只是一个示例，你可能需要根据你的游戏逻辑来调整这个计算方法  
        float maxStamina = _protagonistStats.MaxStamina;
        float amount = Mathf.Max(maxStamina * 0.02f, 1f); // 每次最多恢复1点，或者恢复到最大值  
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


}

public enum StatTypes
{
    MaxHealth,
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
