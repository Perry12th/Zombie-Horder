using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Weapons
{
    [Serializable]
    public struct WeaponStats
    {
        public string Name;
        public float Damage;
        public float BulletsInClip;
        public int ClipSize;
        public int TotalBulletsAvailable;

        public float FireStartDelay;
        public float FireRate;
        public float FireDistance;
        public bool Repeating;

        public LayerMask WeaponHitLayer;
    }

    public class WeaponComponent : MonoBehaviour
    {
        public Transform HandPosition => GripIKLocation;
        [SerializeField] private Transform GripIKLocation;

        public bool WeaponFiring { get; private set; }
        public bool WeaponReloading { get; private set; }

        public WeaponStats WeaponInfo => WeaponStats;

        [SerializeField] protected WeaponStats WeaponStats;

        protected Camera ViewCamera;
        protected WeaponHolder WeaponHolder;
        protected CrosshairScript CrosshairComponent;

        private void Awake()
        {
            ViewCamera = Camera.main;
        }

        public void Initialize(WeaponHolder weaponHolder, CrosshairScript crosshair)
        {
            WeaponHolder = weaponHolder;
            CrosshairComponent = crosshair;
        }

        public virtual void StartFiring()
        {
            WeaponFiring = true;
            if (WeaponStats.Repeating)
            {
                InvokeRepeating(nameof(FireWeapon), WeaponStats.FireStartDelay, WeaponStats.FireRate);
            }
            else
            {
                FireWeapon();
            }
        }

        public virtual void StopFiring()
        {
            WeaponFiring = false;
            CancelInvoke(nameof(FireWeapon));
        }

        protected virtual void FireWeapon()
        {
            Debug.Log("Firing Weapon");
        }

        public void StartReloading()
        {
            WeaponReloading = true;
            ReloadWeapon();
        }

        private void ReloadWeapon()
        {
            int bulletToReload = WeaponStats.TotalBulletsAvailable - WeaponStats.ClipSize;
            if (bulletToReload < 0)
            {
                Debug.Log("Reload - Out of Ammo");
                WeaponStats.BulletsInClip += WeaponStats.TotalBulletsAvailable;
                WeaponStats.TotalBulletsAvailable = 0;
            }
            else
            {
                Debug.Log("Reload");
                WeaponStats.BulletsInClip = WeaponStats.ClipSize;
                WeaponStats.TotalBulletsAvailable -= WeaponStats.ClipSize;
            }
        }

        public void StopReloading()
        {
            WeaponReloading = false;
        }
    }

}