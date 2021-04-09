using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemSlot : MonoBehaviour
{
    private ItemScriptables Item;

    private Button ItemButton;
    private TMP_Text ItemText;

    [SerializeField]
    private ItemSlotAmountWidget AmountWidget;
    [SerializeField]
    private ItemSlotEquippedWidget EquippedWidget;

    private void Awake()
    {
        ItemButton = GetComponent<Button>();
        ItemText = GetComponent<TMP_Text>();
    }

    public void Initialize(ItemScriptables item)
    {
        Item = item;
        ItemText.text = item.name;
        AmountWidget.Initialize(item);
        EquippedWidget.Initialize(item);

        ItemButton.onClick.AddListener(UseItem);
        Item.OnItemDestroyed += OnItemDestroyed;
    }

    public void UseItem()
    {
        Debug.Log($"{Item.Name} - Item Used");
        Item.UseItem(Item.Controller);
    }

    private void OnItemDestroyed()
    {
        Item = null;
        Destroy(gameObject);
    }
}
