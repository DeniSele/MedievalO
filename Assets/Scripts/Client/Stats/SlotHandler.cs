using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text username;
    [SerializeField] private TMP_Text games;
    [SerializeField] private TMP_Text wins;
    [SerializeField] private TMP_Text top5;
    [SerializeField] private TMP_Text kills;

    public void SetText(string username, string games, string wins, string top5, string kills)
    {
        this.username.text = username;
        this.games.text = games;
        this.wins.text = wins;
        this.top5.text = top5;
        this.kills.text = kills;
    }
}
