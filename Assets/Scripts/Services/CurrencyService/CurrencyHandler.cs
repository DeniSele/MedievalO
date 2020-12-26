using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyHandler : MonoBehaviour
{
    #region Fields

    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text gemsText;

    #endregion



    #region Class lifecycle

    private void OnEnable()
    {
        CurrencyService.OnCurrencyUpdated += UpdateCurrencyText;
    }


    private void OnDisable()
    {
        CurrencyService.OnCurrencyUpdated -= UpdateCurrencyText;
    }


    private void UpdateCurrencyText(int coins, int gems)
    {
        coinsText.text = coins.ToString();
        gemsText.text = gems.ToString();
    }

    #endregion
}
