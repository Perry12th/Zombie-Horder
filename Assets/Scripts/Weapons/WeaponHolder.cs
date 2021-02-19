﻿using System.Collections;
using System.Collections.Generic;
using Character;
using Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField]
    GameObject Weapon;
    [SerializeField]
    private Transform WeaponSocket;

    Transform GripLocation;

    //Components
    public PlayerController Controller => PlayerController;
    private PlayerController PlayerController;
    private CrosshairScript PlayerCrosshair;
    private Animator PlayerAnimator;
    
    //Ref
    Camera MainCamera;
    private WeaponComponent EquippedWeapon;

    //Animator Hashes
    private readonly int AimVerticalHash = Animator.StringToHash("AimVertical");
    private readonly int AimHorizontalHash = Animator.StringToHash("AimHorizontal");
    private readonly int IsFiringHash = Animator.StringToHash("IsFiring");
    private readonly int IsReloadingHash = Animator.StringToHash("IsReloading");
    

    void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerAnimator = GetComponent<Animator>();

        MainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(Weapon, WeaponSocket.position, WeaponSocket.rotation);

        if (!spawnedWeapon)
        {
            return;
        }

        spawnedWeapon.transform.parent = WeaponSocket;
        EquippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        GripLocation = EquippedWeapon.HandPosition;

        EquippedWeapon.Initialize(this, PlayerController.CrosshairComponent);
        PlayerEvents.Invoke_OnWeaponEquipped(EquippedWeapon);
    }

    public void OnLook(InputValue delta)
    {
        Vector3 independentMousePosition = MainCamera.ScreenToViewportPoint(PlayerController.CrosshairComponent.CurrentMousePosition);

       
        PlayerAnimator.SetFloat(AimVerticalHash, independentMousePosition.y);
        PlayerAnimator.SetFloat(AimHorizontalHash, independentMousePosition.x);
    }

    public void OnFire(InputValue button)
    {
        Debug.Log("OnFire");

        if (button.isPressed)
        {
            PlayerController.IsFiring = true;
            PlayerAnimator.SetBool("IsFiring", PlayerController.IsFiring);
            EquippedWeapon.StartFiring();
        }
        else
        {
            PlayerController.IsFiring = false;
            PlayerAnimator.SetBool("IsFiring", PlayerController.IsFiring);
            EquippedWeapon.StopFiring();
        }
    }

    public void OnReload(InputValue button)
    {
        StartReloading();
    }

    public void StartReloading()
    {
        PlayerController.IsReloading = true;
        PlayerAnimator.SetBool("IsReloading", PlayerController.IsReloading);
        EquippedWeapon.StartReloading();

        InvokeRepeating(nameof(StopReloading), 0, 0.1f);
    }

    public void StopReloading()
    {
        if (PlayerAnimator.GetBool("IsReloading"))
        {
            return;
        }
        else
        {
            PlayerController.IsReloading = false;
            EquippedWeapon.StopReloading();

            CancelInvoke(nameof(StopReloading));
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, GripLocation.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
