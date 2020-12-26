using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourutineHandler : MonoBehaviour
{
    public void StartGetCourutine(string url, Action<JSONObject> onSuccess = null, Action onFailed = null)
    {
        StartCoroutine(API.GET(url, onSuccess, onFailed));
    }


    public void StartPostCourutine(string url, string body, Action<JSONObject> onSuccess = null, Action onFailed = null)
    {
        StartCoroutine(API.POST(url, body, onSuccess, onFailed));
    }
}
