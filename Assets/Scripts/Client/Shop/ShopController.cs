using API_Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private readonly string shopUrlBase = "https://users-service-medieval.herokuapp.com/v1/store_items";
    private readonly string shopUrlBuy = "https://users-service-medieval.herokuapp.com/v1/store_items/buy";
    private readonly string shopUrlUserItems = "https://users-service-medieval.herokuapp.com/v1/store_items/user/";
    private readonly string shopUrlEquip = "https://users-service-medieval.herokuapp.com/v1/store_items/equip";
    private readonly string shopUrlEquippedItems = "https://users-service-medieval.herokuapp.com/v1/store_items/user/{0}/equipped";

    private ItemsHandler itemsHandler;

    private void Awake()
    {
        itemsHandler = GetComponent<ItemsHandler>();
    }

    private void OnEnable()
    {
        StartCoroutine(API.GET(shopUrlBase, OnRequestSuccess, OnRequestFailed));

        CurrencyService.Instance.GetCurrency();
    }



    public void BuyItem()
    {
        TryBuyItem(itemsHandler.GetCurrentItem);
    }


    public void EquipUneqip()
    {
        EquipUneqipItem(itemsHandler.GetCurrentItem);
    }


    private void EquipUneqipItem(ContentDatabase.Item item)
    {
        string userId = DataHub.Instance.GetStringValue(Keys.PlayerData.userId);

        BuyByUserRequest userCreds = new BuyByUserRequest(userId, item.id);

        string bodyJsonString = JsonUtility.ToJson(userCreds);
        StartCoroutine(API.POST(shopUrlEquip, bodyJsonString, onSuccess: OnEquipSuccess, onFailed: OnBuyFailed));
    }


    private void TryBuyItem(ContentDatabase.Item item)
    {
        string userId = DataHub.Instance.GetStringValue(Keys.PlayerData.userId);

        BuyByUserRequest userCreds = new BuyByUserRequest(userId, item.id);

        string bodyJsonString = JsonUtility.ToJson(userCreds);
        StartCoroutine(API.POST(shopUrlBuy, bodyJsonString, onSuccess: OnBuySuccess, onFailed: OnBuyFailed));
    }


    private void RequestUserItems(JSONObject data)
    {
        string userId = DataHub.Instance.GetStringValue(Keys.PlayerData.userId);
        StartCoroutine(API.GET(shopUrlUserItems + userId, OnUserItemsRequestSuccess, OnRequestFailed));
    }


    private void RequestUserEquippedItems()
    {
        string userId = DataHub.Instance.GetStringValue(Keys.PlayerData.userId);
        StartCoroutine(API.GET(String.Format(shopUrlEquippedItems, userId), OnEquippedItemsRequestSuccess, OnRequestFailed));
    }


    private void OnUserItemsRequestSuccess(JSONObject data)
    {
        if (data.keys.Contains("items"))
        {
            foreach (JSONObject item in data["items"].list)
            {
                ContentDatabase.Instance.SetItemAsOpen(item["item_id"].ToString().Trim('"'));
            }
        }

        RequestUserEquippedItems();
    }

    private void OnEquippedItemsRequestSuccess(JSONObject data)
    {
        if (data.keys.Contains("items"))
        {
            foreach (JSONObject item in data["items"].list)
            {
                ContentDatabase.Instance.SetItemAsEquipped(item["item_id"].ToString().Trim('"'), true);
            }
        }

        itemsHandler.Initialize();
    }


    private void OnEquipSuccess(JSONObject data)
    {
        itemsHandler.SetItemAsEquipped();
        itemsHandler.UpdateView();
    }


    private void OnBuySuccess(JSONObject data)
    {
        itemsHandler.SetItemAsOpen();
        itemsHandler.UpdateView();
        CurrencyService.Instance.GetCurrency();
    }


    private void OnBuyFailed()
    {
        Debug.LogWarning("Operation failed");
    }


    private void OnRequestFailed()
    {
        Debug.LogWarning("Seems like server problems...");
    }


    private void OnRequestSuccess(JSONObject data)
    {
        List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        foreach (JSONObject item in data["results"].list)
        {
            list.Add(item.ToDictionary());
        }

        ContentDatabase.Instance.FillItems(list);
        RequestUserItems(data);
    }
}
