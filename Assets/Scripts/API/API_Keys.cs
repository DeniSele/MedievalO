using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class API_Keys
{
    public static class LoginAndRegistration
    {
        public static string playerToken = "token";
        public static string isAdmin = "isAdmin";
        public static string expiresAt = "expires_at";
        public static string userId = "user_id";
    }


    public static class ItemFields
    {
        public static string id = "id";
        public static string name = "name";
        public static string description = "description";
        public static string type = "type";
        public static string coinsPrice = "coins_price";
        public static string gemsPrice = "gems_price";
        public static string imageId = "image_id";
        public static string isOnSale = "on_sale";
        public static string coinsSalePrice = "sale_coins_price";
        public static string gemsSalePrice = "sale_gems_price";
    }
}
