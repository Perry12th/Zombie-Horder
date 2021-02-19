using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Weapons;
public class WeaponInfoUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI CurrentClipText;
    [SerializeField]
    private TextMeshProUGUI WeaponNameText;
    [SerializeField]
    private TextMeshProUGUI TotalAmmoText;

    private WeaponComponent EquippedWeapon;

    private void OnWeaponEquipped(WeaponComponent weapon)
    {
        EquippedWeapon = weapon;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
