using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionDataManager : MonoBehaviour
{
    [SerializeField] private UpgradeConfigSO _upgrades;

    [Header("Listens on")]
    [SerializeField] private VoidEventChannelSO _sessionStarted;

    [Header("Broadcasting on")]
    [SerializeField] private StringEventChannelSO _passingStats;


    private StatBlock _baseStats;
    private StatBlock _potentialStats;

    // Start is called before the first frame update
    void Start()
    {
        _sessionStarted.OnEventRaised += ProcessSessionData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ProcessSessionData()
    {
        int totalPoints = _upgrades.PointsAvailable;

        int[] potential = _upgrades.GetPotentialLevelsArray();
        potential = potential.Select(x => x + 3).ToArray();

        _baseStats = StatBlock.FromArray(_upgrades.GetBaseLevelsArray());
        _potentialStats = StatBlock.FromArray(DistributePoints(totalPoints, potential));

        StatBlockPair basePotentialPair = new()
        {
            BaseStats = _baseStats,
            PotentialStats = _potentialStats
        };

        string serialized = JsonUtility.ToJson(basePotentialPair);

        _passingStats.RaiseEvent(serialized);
    }

    public static int[] DistributePoints(int totalPoints, int[] maxPerStat)
    {
        int numStats = maxPerStat.Length;
        int[] result = new int[numStats];

        // Step 1: Give each stat the minimum of 1
        for (int i = 0; i < numStats; i++)
        {
            result[i] = 1;
            totalPoints--;
        }

        // Step 2: Create a list of available indices to assign more points
        List<int> indices = Enumerable.Range(0, numStats).ToList();
        System.Random rng = new System.Random();

        // Step 3: Distribute remaining points
        while (totalPoints > 0 && indices.Count > 0)
        {
            int idx = indices[rng.Next(indices.Count)];

            if (result[idx] < maxPerStat[idx])
            {
                result[idx]++;
                totalPoints--;
            }

            // Remove from list if it hit its max
            if (result[idx] >= maxPerStat[idx])
            {
                indices.Remove(idx);
            }
        }

        return result;
    }
}

[Serializable]
public class StatBlockPair
{
    public StatBlock BaseStats;
    public StatBlock PotentialStats;
}
