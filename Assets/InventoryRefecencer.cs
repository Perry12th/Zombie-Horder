using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRefecencer : MonoBehaviour
{
    public static InventoryRefecencer Instance;

    [SerializeField]
    private List<ItemScriptables> ItemList = new List<ItemScriptables>();

    private Dictionary<string, ItemScriptables> ItemDicitonary = new Dictionary<string, ItemScriptables>();

    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        foreach (ItemScriptables itemScriptables in ItemList)
        {
            ItemDicitonary.Add(itemScriptables.name, itemScriptables);
        }
    }


    public ItemScriptables GetItemReference(string itemName) =>
        ItemDicitonary.ContainsKey(itemName) ? ItemDicitonary[itemName] : null;
}
