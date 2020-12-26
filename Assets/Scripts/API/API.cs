using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class API
{
    #region Public methods

    public static IEnumerator POST(string url, string bodyJsonString, Action<JSONObject> onSuccess = null, Action onFailed = null)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);

        string token = DataHub.Instance.GetStringValue(Keys.PlayerData.playerToken);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            Debug.Log(request.downloadHandler.text);

            onFailed?.Invoke();
        }
        else
        {
            Debug.Log(request.downloadHandler.text);

            onSuccess?.Invoke(new JSONObject(request.downloadHandler.text));
        }
    }


    public static IEnumerator GET(string url, Action<JSONObject> onSuccess = null, Action onFailed = null)
    {
        Debug.Log(url);
        string token = DataHub.Instance.GetStringValue(Keys.PlayerData.playerToken);

        var request = new UnityWebRequest(url, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            Debug.Log(request.downloadHandler.text);

            onFailed?.Invoke();
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            onSuccess?.Invoke(new JSONObject(request.downloadHandler.text));
        }
    }

    #endregion
}
