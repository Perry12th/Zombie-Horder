using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class ItemPickUpComponent : MonoBehaviour
{
    [SerializeField]
    private ItemScriptables PickUpItem;

    [Tooltip("Manual Override for Drop Amount, if left at -1 it will use the amount from the scriptable object.")]
    [SerializeField]
    private int Amount = -1;

    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private MeshFilter meshFilter;

    private ItemScriptables ItemInstance;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate();
    }

    private void Instantiate()
    {
        ItemInstance = Instantiate(PickUpItem);
        if (Amount > 0)
        {
            ItemInstance.SetAmount(Amount);
        }

        ApplyMesh();
    }

    private void ApplyMesh()
    {
        if (meshFilter)
        {
            meshFilter.mesh = PickUpItem.ItemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        }
        if (meshRenderer)
        {
            meshRenderer.materials = PickUpItem.ItemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterials;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log($"{ PickUpItem.name} - Picked Up");

        ItemInstance.UseItem(other.GetComponent<PlayerController>());

        Destroy(gameObject);
    }

    private void OnValidate()
    {
        ApplyMesh();
    }
}
