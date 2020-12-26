using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{
    [SerializeField] private SlotHandler slotPrefab;
    [SerializeField] private Transform tableRoot;

    public void FillTable(List<StatsController.Stats> usersStats)
    {
        foreach (Transform child in tableRoot)
        {
            Destroy(child.gameObject);
        }

        foreach (var stats in usersStats)
        {
            var slotsLine = GameObject.Instantiate(slotPrefab, tableRoot);
            slotsLine.SetText(stats.Name, stats.GamesCount, stats.WinsCount, stats.Top5Count, stats.TotalKillsCount);
        }
    }
}
