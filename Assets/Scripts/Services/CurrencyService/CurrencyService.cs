using API_Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyService
{

    #region Fields

    // Get to get; Post to set;
    private readonly string currencyUrlCurrency = "https://users-service-medieval.herokuapp.com/v1/users/{0}/currencies";
    private static CurrencyService instance = null;

    private int coins;
    private int gems;

    public static event Action<int, int> OnCurrencyUpdated;

    #endregion



    #region Properties

    public int GetCoint => coins;
    public int GetGems => gems;

    public static CurrencyService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CurrencyService();
            }
            return instance;
        }
    }

    #endregion



    #region Public methods

    public void AddCurrency(int coins = 0, int gems = 0)
    {
        string userId = DataHub.Instance.GetStringValue(Keys.PlayerData.userId);
        GrantCurrenciesRequest userLoginCredits = new GrantCurrenciesRequest(userId, coins, gems);
        string bodyJsonString = JsonUtility.ToJson(userLoginCredits);
        GameManager.courutineHandler.StartPostCourutine(String.Format(currencyUrlCurrency, userId), bodyJsonString, (JSONObject data) =>
        {
            GetCurrency();
        });
    }


    public void GetCurrency()
    {
        string userId = DataHub.Instance.GetStringValue(Keys.PlayerData.userId);
        GameManager.courutineHandler.StartGetCourutine(String.Format(currencyUrlCurrency, userId), OnSuccess);
    }

    #endregion



    #region Event handlers

    private void OnSuccess(JSONObject data)
    {
        coins = data.keys.Contains("coins") ? Int32.Parse(data["coins"].ToString()) : 0;
        gems = data.keys.Contains("gems") ? Int32.Parse(data["gems"].ToString()) : 0;
        OnCurrencyUpdated?.Invoke(coins, gems);
    }

    #endregion
}
