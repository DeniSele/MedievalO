using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemsHandler : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform itemsRoot;
    [SerializeField] private Item itemPrefab;

    [Header("Selected item")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    [Space(10)]
    [SerializeField] private Transform coinsRoot;
    [SerializeField] private Transform gemsRoot;

    [SerializeField] private TMP_Text coinsPrice;
    [SerializeField] private TMP_Text gemsPrice;

    [Space(10)]
    [SerializeField] private Button buyButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button unequipButton;

    private ContentDatabase.Item.ItemType currentType;
    private ContentDatabase.Item currentItem;

    public ContentDatabase.Item GetCurrentItem => currentItem;

    #endregion



    #region Class lifecycle

    private void OnDisable()
    {
        ClearAllItems();
    }

    #endregion



    #region Public methods

    public void Initialize()
    {
        FillByType(ContentDatabase.Item.ItemType.Weapon, true);
    }


    public void FillByType(int type)
    {
        FillByType((ContentDatabase.Item.ItemType)type);
    }


    public void UpdateView()
    {
        FillByType(currentType);
        SelectItem(currentItem);
    }


    public void SetItemAsOpen(string id = null)
    {
        if (id == null)
        {
            id = currentItem.id;
        }

        ContentDatabase.Instance.SetItemAsOpen(id);
    }


    public void SetItemAsEquipped(string id = null)
    {
        if (id == null)
        {
            id = currentItem.id;
        }

        ContentDatabase.Instance.SetItemAsEquipped(id);
    }


    public void SelectItem(ContentDatabase.Item item)
    {
        Sprite sprite = ResourcesLoader.Instance.GetResourceByName($"Textures/{item.type}/{item.imageId}");

        itemImage.sprite = sprite ? sprite : ResourcesLoader.Instance.GetResourceByName($"Textures/notfound");

        itemName.text = item.name;
        itemDescription.text = item.description;

        gemsPrice.text = $"{item.gemsPrice}";
        coinsPrice.text = $"{item.coinsPrice}";

        coinsRoot.gameObject.SetActive(!item.IsPremium);
        gemsRoot.gameObject.SetActive(item.IsPremium);

        currentItem = item;

        buyButton.gameObject.SetActive(!item.isOpen);
        equipButton.gameObject.SetActive(item.isOpen && !item.isEquipped);
        unequipButton.gameObject.SetActive(item.isOpen && item.isEquipped);
    }


    #endregion



    #region Private methods

    private void FillByType(ContentDatabase.Item.ItemType itemType, bool selectFirst = false)
    {
        List<ContentDatabase.Item> typeItems = ContentDatabase.Instance.ContentItems.Where(item => item.type == itemType).ToList();

        if(typeItems.Count == 0)
        {
            return;
        }

        ClearAllItems();

        foreach (var item in typeItems)
        {
            var newItem = Instantiate(itemPrefab, itemsRoot);
            newItem.Initialize(item, this);
        }

        if (selectFirst)
        {
            SelectItem(typeItems[0]);
        }
    }


    private void ClearAllItems()
    {
        foreach(var child in itemsRoot.GetComponentsInChildren<Item>())
        {
            Destroy(child.gameObject);
        }
    }

    #endregion
}
