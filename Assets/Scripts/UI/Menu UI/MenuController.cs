using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private string StartingMenu = "Main Menu";
    [SerializeField]
    private string RootMenu = "Main Menu";
    private MenuWidget ActiveMenu;
    [SerializeField]
    private Dictionary<string, MenuWidget> Menus = new Dictionary<string, MenuWidget>();
    // Start is called before the first frame update
    void Start()
    {
        DisableAllMenus();
        EnableMenu(StartingMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMenu(string menuName, MenuWidget menuWidget)
    {
        if (Menus.ContainsKey(menuName))
        {
            Debug.LogError("Tried to add existing menu widget into dictionary");
            return;
        }

        if (menuWidget == null)
        {
            return;
        }

        Menus.Add(menuName, menuWidget);
    }

    public void EnableMenu(string menuName)
    {
        if (Menus.ContainsKey(menuName))
        {
            DisableActiveMenu();

            ActiveMenu = Menus[menuName];
            ActiveMenu.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Menu bot found in Dictionary");
        }
    }

    public void ReturnToRootMenu()
    {
        EnableMenu(RootMenu);
    }

    private void DisableActiveMenu()
    {
        if (ActiveMenu)
        {
            ActiveMenu.gameObject.SetActive(false);
        }     
    }

    private void DisableAllMenus()
    {
        foreach(MenuWidget menu in Menus.Values)
        {
            menu.gameObject.SetActive(false);
        }
    }
}

public abstract class MenuWidget : MonoBehaviour
{
    [SerializeField]
    private string MenuName;
    protected MenuController MenuController;

    private void Awake()
    {
        MenuController = FindObjectOfType<MenuController>();
        if (MenuController)
        {
            MenuController.AddMenu(MenuName, this);
        }
        else
        {
            Debug.LogError("Message Controller not found");
        }
    }

    public void ReturnToRootMenu()
    {
        MenuController.ReturnToRootMenu();
    }
}

