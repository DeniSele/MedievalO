using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public class Stats
    {
        private string name;
        private string gamesCount;
        private string winsCount;
        private string top5Count;
        private string totalKillsCount;

        public string Name => name;
        public string GamesCount => gamesCount;
        public string WinsCount => winsCount;
        public string Top5Count => top5Count;
        public string TotalKillsCount => totalKillsCount;

        public Stats(string name, string gamesCount, string winsCount, string top5Count, string totalKillsCount)
        {
            this.name = name;
            this.gamesCount = gamesCount;
            this.winsCount = winsCount;
            this.top5Count = top5Count;
            this.totalKillsCount = totalKillsCount;
        }
    }

    private readonly string getUsersStatsUrlBase = "https://users-service-medieval.herokuapp.com/v1/users?_order_by=wins+desc,kills+desc";

    private List<Stats> usersStats = new List<Stats>();

    private StatsHandler statsHandler;

    private void Awake()
    {
        statsHandler = GetComponent<StatsHandler>();
    }

    private void OnEnable()
    {
        StartCoroutine(API.GET(getUsersStatsUrlBase, OnRequestSuccess, OnRequestFailed));
    }


    private void UpdateStatsList(JSONObject data)
    {
        usersStats.Clear();

        foreach (JSONObject item in data["results"].list)
        {
            string name = item["name"].ToString();

            JSONObject stats = item["stats"];
            bool isEmpty = stats.ToDictionary().Count == 0;

            string games = isEmpty ? "0" : stats["games"].ToString();
            string wins = isEmpty ? "0" : stats["wins"].ToString();
            string top5 = isEmpty ? "0" : stats["top5"].ToString();
            string kills = isEmpty ? "0" : stats["kills"].ToString();

            usersStats.Add(new Stats(
                name: name,
                gamesCount: games,
                winsCount: wins,
                top5Count: top5,
                totalKillsCount: kills
                ));
        }

        statsHandler.FillTable(usersStats);
    }


    private void OnRequestFailed()
    {
        Debug.LogWarning("Seems like server problems...");
    }


    private void OnRequestSuccess(JSONObject data)
    {
        UpdateStatsList(data);
    }
}
