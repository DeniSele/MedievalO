using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text gemsText;

    private void OnEnable()
    {
        CurrencyService.OnCurrencyUpdated += UpdateCurrencyText;
    }

    private void OnDisable()
    {
        CurrencyService.OnCurrencyUpdated -= UpdateCurrencyText;
    }

    private void Start()
    {
        //CurrencyService.Instance.AddCurrency(1000, 500);
    }

    private void UpdateCurrencyText(int coins, int gems)
    {
        coinsText.text = coins.ToString();
        gemsText.text = gems.ToString();
    }
}
