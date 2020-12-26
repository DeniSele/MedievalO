using System;

namespace API_Classes
{
    [Serializable]
    public class UserRegistrationCredits
    {
        public string email;
        public string name;
        public string password;

        public UserRegistrationCredits(string email, string name, string password)
        {
            this.email = email;
            this.name = name;
            this.password = password;
        }
    }


    [Serializable]
    public class UserLoginCredits
    {
        public string id;
        public string password;

        public UserLoginCredits(string name, string password)
        {
            this.id = name;
            this.password = password;
        }
    }


    [Serializable]
    public class BuyByUserRequest
    {
        public string user_id;
        public string item_id;

        public BuyByUserRequest(string token, string item_id)
        {
            this.user_id = token;
            this.item_id = item_id;
        }
    }


    [Serializable]
    public class GrantCurrenciesRequest
    {
        public string user_id;
        public int add_coins;
        public int add_gems;

        public GrantCurrenciesRequest(string token, int coins, int gems)
        {
            this.user_id = token;
            this.add_coins = coins;
            this.add_gems = gems;
        }
    }
}
