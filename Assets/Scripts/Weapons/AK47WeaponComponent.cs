using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class AK47WeaponComponent : WeaponComponent
    {
        

        private Vector3 HitLocation;

        
        protected new void FireWeapon()
        {
            base.FireWeapon();

            if (WeaponStats.BulletsInClip > 0 && !WeaponReloading && !WeaponHolder.Controller.IsRunning)
            {
                Ray screnRay = ViewCamera.ScreenPointToRay(new Vector3(CrosshairComponent.CurrentMousePosition.x,
                CrosshairComponent.CurrentMousePosition.y, 0));

                if (!Physics.Raycast(screnRay, out RaycastHit hit,
                    WeaponStats.FireDistance, WeaponStats.WeaponHitLayer)) return;

                HitLocation = hit.point;

                Vector3 hitDirection = hit.point - ViewCamera.transform.position;
                Debug.DrawRay(ViewCamera.transform.position, hitDirection.normalized * WeaponStats.FireDistance, Color.red);
            }

            else if (WeaponStats.BulletsInClip <= 0)
            {
                if (!WeaponHolder) return;

                WeaponHolder.StartReloading();
            }
            
        }

        private void OnDrawGizmos()
        {
            if (HitLocation != Vector3.zero)
            {
                Gizmos.DrawWireSphere(HitLocation, 0.2f);
            }
        }
    }
}