using System.Collections;
using System.Collections.Generic;
using Character;
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
    PlayerController PlayerController;
    Animator PlayerAnimator;
    
    //Ref
    Camera MainCamera;

    //Animator Hashes
    private readonly int AimVertical = Animator.StringToHash("AimVertical");
    private readonly int AimHorizontal = Animator.StringToHash("AimHorizontal");

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
        WeaponComponent weapon = spawnedWeapon.GetComponent<WeaponComponent>();
        GripLocation = weapon.HandPosition;
    }

    public void OnLook(InputValue delta)
    {
        Vector3 independentMousePosition = MainCamera.ScreenToViewportPoint(PlayerController.CrosshairComponent.CurrentMousePosition);

       
        PlayerAnimator.SetFloat(AimVertical, independentMousePosition.y);
        PlayerAnimator.SetFloat(AimHorizontal, independentMousePosition.x);
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
