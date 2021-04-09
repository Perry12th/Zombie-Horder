using Character.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Health;
using System;

namespace Character
{
    public class PlayerController : MonoBehaviour, IPausable
    {
        public CrossHairScript CrossHair => CrossHairComponent;
        [SerializeField] private CrossHairScript CrossHairComponent;

       

        public HealthCompoment Health => HealthComponent;
        private HealthCompoment HealthComponent;

        public InventoryComponent Inventory => InventoryComponent;
        private InventoryComponent InventoryComponent;

        public WeaponHolder Weapon => WeaponHolder;
        private WeaponHolder WeaponHolder;

        private GameUIController GameUIController;
        private PlayerInput PlayerInput;
        
        public bool IsFiring;
        public bool IsReloading;
        public bool IsJumping;
        public bool IsRunning;
        public bool InInventory;

        private void Awake()
        {
            if (GameUIController == null)
            {
                GameUIController = FindObjectOfType<GameUIController>();
            }
            if (PlayerInput == null)
            {
                PlayerInput = GetComponent<PlayerInput>();
            }
            if (Health == null)
            {
                HealthComponent = GetComponent<HealthCompoment>();
            }
            if (Weapon == null)
            {
                WeaponHolder = GetComponent<WeaponHolder>();
            }
        }

        public void OnPauseGame()
        {
            PauseManager.Instance.PauseGame();
        }

        public void OnUnPauseGame()
        {
            PauseManager.Instance.UnPauseGame();
        }

        public void OnInventory(InputValue Button)
        {
            if (InInventory)
            {
                InInventory = false;
                OpenInventory(false);
            }
            else
            {
                InInventory = true;
                OpenInventory(true);
            }
        }

        private void OpenInventory(bool open)
        {
            if (open)
            {
                PauseManager.Instance.PauseGame();
                GameUIController.EnableInventoryMenu();
            }
            else
            {
                PauseManager.Instance.UnPauseGame();
                GameUIController.EnableGameMenu();
            }
        }

        public void PauseGame()
        {
           
            GameUIController.EnablePauseMenu();
            if (PlayerInput)
            {
                PlayerInput.SwitchCurrentActionMap("PauseActionMap");
            }
        }

        public void UnPauseGame()
        {
            
            GameUIController.EnableGameMenu();
            if (PlayerInput)
            {
                PlayerInput.SwitchCurrentActionMap("PlayerActionMap");
            }
        }
    }
}
