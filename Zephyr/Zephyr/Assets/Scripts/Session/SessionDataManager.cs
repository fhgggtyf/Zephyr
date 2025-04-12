using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionDataManager : MonoBehaviour
{
    [SerializeField] private SaveSystem saveSystem;

    [Header("Listening on")]
    [SerializeField] private VoidEventChannelSO _receiveStatsChannel;

    // Start is called before the first frame update
    private void Start()
    {
        _receiveStatsChannel.OnEventRaised += SendStats;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SendStats()
    {

    }
}
