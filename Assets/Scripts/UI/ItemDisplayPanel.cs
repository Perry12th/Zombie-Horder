using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject ItemSlot;
    private RectTransform RectTransform;
    // Start is called before the first frame update
    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        WipeChildern();
    }

    public void PopulatePanel(List<ItemScriptables> itemList)
    {
        WipeChildern();

        foreach(ItemScriptables item in itemList)
        {
            ItemSlot icon = Instantiate(ItemSlot, RectTransform).GetComponent<ItemSlot>();
        }
    }

    private void WipeChildern()
    {
        foreach (RectTransform child in RectTransform)
        {
            Destroy(child.gameObject);
        }
        RectTransform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
