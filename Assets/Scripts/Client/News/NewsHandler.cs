using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text mainText;

    public void SetNewsInfo(string title, string mainText)
    {
        this.title.text = title;
        this.mainText.text = mainText;
    }
}
