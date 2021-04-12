using Character.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Health;
using System;
using Weapons;
using System.Collections.Generic;
using System.Linq;

namespace Character
{
    public class PlayerController : MonoBehaviour, IPausable, ISavable
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

        public void OnSave(InputValue button)
        {
            SaveSystem.Instance.SaveGame();
        }

        public void OnLoad(InputValue button)
        {
            SaveSystem.Instance.LoadGame();
        }


        public SaveDataBase SaveData()
        {
            PlayerSaveData saveData = new PlayerSaveData()
            {
                Name = gameObject.name,
                Position = transform.position,
                Rotation = transform.rotation,
                CurrentHealth = HealthComponent.Health,
                EquippedWeaponData = new WeaponSaveData(WeaponHolder.CurrentWeapon.WeaponInformation),

            };

            var itemSaveList = Inventory.GetItemList().Select(item => new ItemSaveData(item)).ToList();
            saveData.ItemList = itemSaveList;

            return saveData;
            
        }

        public void LoadData(SaveDataBase saveData)
        {
            PlayerSaveData playerSave = (PlayerSaveData)saveData;

            if (playerSave == null) return;

            Transform playerTransform = transform;
            playerTransform.position = playerSave.Position;
            playerTransform.rotation = playerSave.Rotation;

            Health.SetCurrentHealth(playerSave.CurrentHealth);

            foreach (ItemSaveData itemSave in playerSave.ItemList)
            {
                ItemScriptables item = InventoryRefecencer.Instance.GetItemReference(itemSave.Name);
                Inventory.AddItem(item, itemSave.Amount);
            }

            WeaponScriptable weapon = (WeaponScriptable)(Inventory.FindItem(playerSave.EquippedWeaponData.Name));
            weapon.WeaponStats = playerSave.EquippedWeaponData.WeaponStats;
            WeaponHolder.EquipWeapon(weapon);
        }

        
        [Serializable]
        public class PlayerSaveData : SaveDataBase
        {
            public float CurrentHealth;
            public Vector3 Position;
            public Quaternion Rotation;
            public WeaponSaveData EquippedWeaponData;
            public List<ItemSaveData> ItemList;

        }

        [Serializable]
        public class WeaponSaveData : SaveDataBase
        {
            
            public WeaponStats WeaponStats;

            public WeaponSaveData(WeaponStats weaponStats)
            {
                Name = WeaponStats.WeaponName;
                WeaponStats = weaponStats;
            }
        }

        [Serializable]
        public class ItemSaveData : SaveDataBase
        {
            public int Amount;

            public ItemSaveData(ItemScriptables item)
            {
                Name = item.name;
                Amount = item.Amount;
            }
        }


    }


}
