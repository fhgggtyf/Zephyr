using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionStatsManager : MonoBehaviour
{
    [SerializeField] private StatsConfigSO _playerStats;

    [Header("Listening on")]
    [SerializeField] private TransformEventChannelSO _playerInstantiated;
    [SerializeField] private StringEventChannelSO _statsPairChannel;

    [Header("Broadcasting on")]
    [SerializeField] private SOEventChannelSO _stats;


    private StatBlock _baseStats;
    private StatBlock _potentialStats;

    // Start is called before the first frame update
    private void Start()
    {
        _statsPairChannel.OnEventRaised += ParseStats;
        _playerInstantiated.OnEventRaised += SendStats;

        //on death trigger recalculation of base and potential (unimplemented)
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ParseStats(string serialized)
    {
        StatBlockPair deserialized = JsonUtility.FromJson<StatBlockPair>(serialized);
        _baseStats = deserialized.BaseStats;
        _potentialStats = deserialized.PotentialStats;


    }

    private void SendStats(Transform playerTransform)
    {
        _stats.RaiseEvent(_playerStats);
    }
}
