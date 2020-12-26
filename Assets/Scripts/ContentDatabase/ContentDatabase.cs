using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentDatabase
{
    #region Nested types

    public class Item
    {
        public enum ItemType
        {
            None = 0,
            Weapon = 1,
            Armor = 2,
            Helmet = 3,
            Shield = 4
        }

        public string id;
        public string name;
        public string description;
        public ItemType type;
        
        public int coinsPrice;
        public int gemsPrice;

        public int coinsSalePrice;
        public int gemsSalePrice;

        public string imageId;

        public bool isOpen;
        public bool isOnSale;

        public bool isEquipped;

        public bool IsPremium => gemsPrice != 0;


        public Item(string id, string name, string description, string type, string imageId, string coinsPrice, string gemsPrice, string isOnSale, string coinsSalePrice, string gemsSalePrice)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.type = (ItemType)int.Parse(type);
            this.imageId = imageId;

            this.coinsPrice = int.Parse(coinsPrice);
            this.gemsPrice = int.Parse(gemsPrice);
            this.isOnSale = bool.Parse(isOnSale);

            this.coinsSalePrice = int.Parse(coinsSalePrice);
            this.gemsSalePrice = int.Parse(gemsSalePrice);
        }


        public override string ToString()
        {
            string result = $"{id}, {name}, {description}, {type}, {coinsPrice}, {gemsPrice}, {imageId}, {IsPremium}";
            return result;
        }
    }

    #endregion



    #region Fields

    private static ContentDatabase instance = null;
    private List<Item> contentItems = new List<Item>();

    #endregion



    #region Properties

    public List<Item> ContentItems => contentItems;

    public static ContentDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ContentDatabase();
            }
            return instance;
        }
    }

    #endregion



    #region Public methods

    public void SetItemAsEquipped(string id, bool setState = false)
    {
        Item itemToChange = contentItems.First(item => item.id == id);
        itemToChange.isEquipped = setState ? true : !itemToChange.isEquipped;
    }


    public void SetItemAsOpen(string id)
    {
        Item itemToChange = contentItems.First(item => item.id == id);
        itemToChange.isOpen = true;
    }


    public void FillItems(List<Dictionary<string, string>> items)
    {
        contentItems.Clear();

        foreach(var item in items)
        {
            string gems = item.ContainsKey(API_Keys.ItemFields.gemsPrice) ? item[API_Keys.ItemFields.gemsPrice] : "0";
            string coins = item.ContainsKey(API_Keys.ItemFields.coinsPrice) ? item[API_Keys.ItemFields.coinsPrice] : "0";

            string isOnSale = item.ContainsKey(API_Keys.ItemFields.isOnSale) ? item[API_Keys.ItemFields.isOnSale] : "false";
            string coinsSalePrice = item.ContainsKey(API_Keys.ItemFields.coinsSalePrice) ? item[API_Keys.ItemFields.coinsSalePrice] : "0";
            string gemsSalePrice = item.ContainsKey(API_Keys.ItemFields.gemsSalePrice) ? item[API_Keys.ItemFields.gemsSalePrice] : "0";

            Item newItem = new Item(
                item[API_Keys.ItemFields.id],
                item[API_Keys.ItemFields.name],
                item[API_Keys.ItemFields.description],
                item[API_Keys.ItemFields.type],
                item[API_Keys.ItemFields.imageId],
                coins,
                gems,
                isOnSale,
                coinsSalePrice,
                gemsSalePrice
                );

            Debug.Log(newItem.ToString());
            contentItems.Add(newItem);
        }
    }

    #endregion
}
