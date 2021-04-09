using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using System.Linq;

public class InventoryWidget : GameHUDWidget
{
    private ItemDisplayPanel ItemDisplayPanel;
    private List<CategorySelectButton> CategorySelectButtons;

    private PlayerController PlayerController;

    private void Awake()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        CategorySelectButtons = GetComponentsInChildren<CategorySelectButton>().ToList();
        ItemDisplayPanel = GetComponentInChildren<ItemDisplayPanel>();
        foreach (CategorySelectButton button in CategorySelectButtons)
        {
            button.Initialize(this);
        }
    }


    private void OnEnable()
    {
        if (!PlayerController || !PlayerController.Inventory) return;
        if (PlayerController.Inventory.GetItemCount() <= 0) return;

        ItemDisplayPanel.PopulatePanel(PlayerController.Inventory.GetItemsOfCategory(ItemCategory.None));

    }
    internal void SelectCategory(ItemCategory category)
    {
        if (!PlayerController || !PlayerController.Inventory) return;
        if (PlayerController.Inventory.GetItemCount() <= 0) return;

        ItemDisplayPanel.PopulatePanel(PlayerController.Inventory.GetItemsOfCategory(category));
    }
}
