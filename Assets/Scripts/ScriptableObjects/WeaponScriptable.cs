using Character;
using UnityEngine;
using Weapons;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Weapon", order = 1)]
public class WeaponScriptable : EquipableScriptable
{
    public WeaponStats WeaponStats;

    public override void UseItem(PlayerController controller)
    {
        
        if (Equipped)
        {
            controller.Weapon.UnEquipItem();
        }
        else
        {
            controller.Weapon.EquipWeapon(this);
        }
        base.UseItem(controller);
    }
}
