using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private Image saleIcon;

    [SerializeField] private Button button;

    private ContentDatabase.Item currentItem;
    private ItemsHandler itemsHandler;

    #endregion



    #region Class lifecycle

    private void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }


    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    #endregion



    #region Public methods

    public void Initialize(ContentDatabase.Item item, ItemsHandler itemsHandler)
    {
        Sprite sprite = ResourcesLoader.Instance.GetResourceByName($"Textures/{item.type}/{item.imageId}");
        icon.sprite = sprite ? sprite : ResourcesLoader.Instance.GetResourceByName($"Textures/notfound");

        this.itemsHandler = itemsHandler;
        currentItem = item;
        nameLabel.text = item.name;

        saleIcon.gameObject.SetActive(item.isOnSale);
    }

    #endregion



    #region Event handlers

    private void OnClick()
    {
        itemsHandler.SelectItem(currentItem);
    }

    #endregion
}
