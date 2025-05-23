using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "Save System", menuName = "Save System/Save")]
public class SaveSystem : ScriptableObject
{
    [SerializeField] private VoidEventChannelSO _saveSettingsEvent = default;
    [SerializeField] private LoadEventChannelSO _loadLocation = default;
    [SerializeField] private LoadEventChannelSO _loadHome = default;
    [SerializeField] private VoidEventChannelSO _saveUpgradesEvent = default;
    [SerializeField] private InventorySO _playerInventory = default;
    [SerializeField] private SettingsSO _currentSettings = default;
    [SerializeField] private UpgradeConfigSO _upgrades = default;
    //[SerializeField] private QuestManagerSO _questManagerSO = default;

    public string saveFilename = "save.zyp";
    public string backupSaveFilename = "save.zyp.bak";
    public Save saveData = new Save();

    void OnEnable()
    {
        
        _saveSettingsEvent.OnEventRaised += SaveSettings;
        _saveUpgradesEvent.OnEventRaised += SaveUpgrades;
        _loadLocation.OnLoadingRequested += CacheLoadLocations;
        _loadHome.OnLoadingRequested += CacheLoadLocations;
    }

    void OnDisable()
    {
        _saveSettingsEvent.OnEventRaised -= SaveSettings;
        _saveUpgradesEvent.OnEventRaised -= SaveUpgrades;
        _loadLocation.OnLoadingRequested -= CacheLoadLocations;
        _loadHome.OnLoadingRequested -= CacheLoadLocations;
    }

    private void CacheLoadLocations(GameSceneSO locationToLoad, bool showLoadingScreen, bool fadeScreen)
    {
        LocationSO locationSO = locationToLoad as LocationSO;
        if (locationSO)
        {
            saveData._locationId = locationSO.Guid;
        }

        SaveDataToDisk();
    }

    public bool LoadSaveDataFromDisk()
    {
        if (FileManager.LoadFromFile(saveFilename, out var json))
        {
            saveData.LoadFromJson(json);
            return true;
        }

        return false;
    }

    public IEnumerator LoadSavedInventory()
    {
        _playerInventory.Items.Clear();
        foreach (var serializedItemStack in saveData._itemStacks)
        {
            var loadItemOperationHandle = Addressables.LoadAssetAsync<ItemSO>(serializedItemStack.itemGuid);
            yield return loadItemOperationHandle;
            if (loadItemOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                var itemSO = loadItemOperationHandle.Result;
                _playerInventory.Add(itemSO, serializedItemStack.amount);
            }
        }
    }
    //public void LoadSavedQuestlineStatus()
    //{
    //	_questManagerSO.SetFinishedQuestlineItemsFromSave(saveData._finishedQuestlineItemsGUIds);

    //}

    public void SaveDataToDisk()
    {
        saveData._itemStacks.Clear();
        foreach (var itemStack in _playerInventory.Items)
        {
            if (itemStack.Item != null)
            {
                saveData._itemStacks.Add(new SerializedItemStack(itemStack.Item.Guid, itemStack.Amount));
            }
        }

        //saveData._finishedQuestlineItemsGUIds.Clear();
        //foreach (var item in _questManagerSO.GetFinishedQuestlineItemsGUIds())
        //{
        //	saveData._finishedQuestlineItemsGUIds.Add(item);
        //}

        if (FileManager.MoveFile(saveFilename, backupSaveFilename))
        {
            if (FileManager.WriteToFile(saveFilename, saveData.ToJson()))
            {
                Debug.Log("Save successful " + saveFilename);
            }
        }
    }

    public void WriteEmptySaveFile()
    {
        FileManager.WriteToFile(saveFilename, "");

    }
    public void SetNewGameData()
    {
        FileManager.WriteToFile(saveFilename, "");
        _playerInventory.Init();
        //_questManagerSO.ResetQuestlines();

        SaveDataToDisk();

    }
    void SaveSettings()
    {
        saveData.SaveSettings(_currentSettings);
    }

    void SaveUpgrades()
    {
        saveData.SaveUpgrades(_upgrades);
    }
}
