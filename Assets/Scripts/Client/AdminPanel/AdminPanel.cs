using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminPanel : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Button button;
    [SerializeField] private GameObject panel;

    [Header("Currency panel")]
    [SerializeField] private Button buttonCurrency;

    [SerializeField] private Text textCoins;
    [SerializeField] private Text textGems;


    private void OnEnable()
    {
        panel.SetActive(false);
        button.onClick.AddListener(SetPanelActivity);
        buttonCurrency.onClick.AddListener(AddCurrency);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(SetPanelActivity);
        buttonCurrency.onClick.RemoveListener(AddCurrency);
    }


    private void AddCurrency()
    {
        int coins = int.Parse(textCoins.text);
        int gems = int.Parse(textGems.text);

        CurrencyService.Instance.AddCurrency(coins, gems);
    }


    private void SetPanelActivity()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
