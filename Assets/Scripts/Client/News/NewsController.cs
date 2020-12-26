using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsController : MonoBehaviour
{
    #region Fields

    private readonly string getNewsUrlBase = "https://users-service-medieval.herokuapp.com/v1/news";

    private NewsHandler newsHandler;

    #endregion



    #region Class lifecycle

    private void Awake()
    {
        newsHandler = GetComponent<NewsHandler>();
    }


    private void OnEnable()
    {
        StartCoroutine(API.GET(getNewsUrlBase, OnRequestSuccess, OnRequestFailed));
    }

    #endregion



    #region Event handlers

    private void OnRequestFailed()
    {
        Debug.LogWarning("Server problems");
    }


    private void OnRequestSuccess(JSONObject data)
    {
        int count = data["results"].list.Count;
        if (count > 0)
        {
            JSONObject newsItem = data["results"].list[count - 1];

            string title = newsItem["title"].ToString();
            string description = newsItem["description"].ToString();

            newsHandler.SetNewsInfo(title, description);
        }
    }

    #endregion
}
