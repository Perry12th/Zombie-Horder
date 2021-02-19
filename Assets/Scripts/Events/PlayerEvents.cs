using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
public class PlayerEvents
{ 

    public delegate void WeaponEquipped(bool enable);

    public static event WeaponEquipped OnWeaponEquipped;

    public static void Invoke_OnWeaponEquipped(WeaponComponent weapon)
    {
        OnWeaponEquipped?.Invoke(weapon);
    }
}
