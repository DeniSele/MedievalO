using API_Classes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    #region Fields

    private readonly string usersUrlBase = "https://users-service-medieval.herokuapp.com/v1/users";
    private readonly string usersUrlLogin = "https://users-service-medieval.herokuapp.com/v1/users/login";

    private readonly string successRegistrationString = "Registration success!";
    private readonly string failedRegistrationString = "Registration failed!";

    private readonly string failedLoginString = "Login failed!";

    private readonly string invalidStringValidation = "Fields should consist only of letters, numbers or underscore";

    [Header("Menu screen")]
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject adminPanel;

    [Header("Register screen")]
    [SerializeField] private GameObject registerScreen;
    [SerializeField] private TMP_Text warningRegString;

    [Header("Login screen")]
    [SerializeField] private GameObject loginScreen;
    [SerializeField] private TMP_Text warningLogString;

    [Space(10)]
    [Header("Registration input fields")]
    [SerializeField] private InputField nicknameRegInput;
    [SerializeField] private InputField emailRegInput;
    [SerializeField] private InputField passwordRegInput;

    [Space(10)]
    [Header("Login input fields")]
    [SerializeField] private InputField nicknameLogInput;
    [SerializeField] private InputField passwordLogInput;

    private Dictionary<string, string> resultDictionary = new Dictionary<string, string>();

    #endregion



    #region Class lifecycle

    private void Awake()
    {
        if (DataHub.Instance.HasKey(Keys.PlayerData.expiresAt))
        {
            var date = DateTime.Parse(DataHub.Instance.GetStringValue(Keys.PlayerData.expiresAt));
            if (date > DateTime.Now)
            {
                MoveToTheMainMenu();
                SetAdminActivity();
            }
        }
    }

    #endregion



    #region Public methods

    public void RegisterMethod()
    {
        warningRegString.text = "";

        string nickname = nicknameRegInput.text;
        string email = emailRegInput.text;
        string password = passwordRegInput.text;

        if (!ValidateStrings(new List<string>
        {
            nickname,
            password
        }))
        {
            warningRegString.text = invalidStringValidation;
            return;
        }

        UserRegistrationCredits userCreds = new UserRegistrationCredits(email, nickname, password);
        string bodyJsonString = JsonUtility.ToJson(userCreds);
        StartCoroutine(API.POST(usersUrlBase, bodyJsonString, onSuccess: OnRegisterSuccess, onFailed: OnRegisterFailed));
    }


    public void LoginMethod()
    {
        warningLogString.text = "";

        string nickname = nicknameLogInput.text;
        string password = passwordLogInput.text;

        if (!ValidateStrings(new List<string>
        {
            nickname,
            password
        }))
        {
            warningLogString.text = invalidStringValidation;
            return;
        }

        UserLoginCredits userLoginCredits = new UserLoginCredits(nickname, password);
        string bodyJsonString = JsonUtility.ToJson(userLoginCredits);
        StartCoroutine(API.POST(usersUrlLogin, bodyJsonString, onSuccess: OnLoginSuccess, onFailed: OnLoginFailed));
    }

    #endregion



    #region Private methods

    private bool ValidateStrings(List<string> values)
    {
        foreach (string value in values)
        {
            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9_]+$") || value == "")
            {
                return false;
            }
        }
        return true;
    }


    private void MoveToTheMainMenu()
    {
        mainMenuScreen.SetActive(true);
        loginScreen.SetActive(false);
    }


    private void SetAdminActivity()
    {
        adminPanel.SetActive(bool.Parse(DataHub.Instance.GetStringValue(Keys.PlayerData.isAdmin)));
    }

    #endregion



    #region Event handlers

    private void OnRegisterSuccess(JSONObject data)
    {
        registerScreen.SetActive(false);
        loginScreen.SetActive(true);
        warningLogString.text = successRegistrationString;
    }


    private void OnRegisterFailed()
    {
        warningRegString.text = failedRegistrationString;
    }


    private void OnLoginSuccess(JSONObject data)
    {
        MoveToTheMainMenu();

        resultDictionary = data.ToDictionary();

        DataHub.Instance.SetStringValue(Keys.PlayerData.playerToken, resultDictionary[API_Keys.LoginAndRegistration.playerToken]);
        DataHub.Instance.SetStringValue(Keys.PlayerData.expiresAt, resultDictionary[API_Keys.LoginAndRegistration.expiresAt]);
        DataHub.Instance.SetStringValue(Keys.PlayerData.userId, resultDictionary[API_Keys.LoginAndRegistration.userId]);

        if (resultDictionary.ContainsKey(API_Keys.LoginAndRegistration.isAdmin))
        {
            DataHub.Instance.SetStringValue(Keys.PlayerData.isAdmin, resultDictionary[API_Keys.LoginAndRegistration.isAdmin]);
        }
    }


    private void OnLoginFailed()
    {
        warningLogString.text = failedLoginString;
    }

    #endregion
}
