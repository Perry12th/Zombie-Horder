using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadItemWidget : MonoBehaviour
{
    [SerializeField]
    private TMP_Text SaveNameText;
    private string SaveName;
    private LoadSelectMenuController loadSelectMenu;

    public void Awake()
    {
        loadSelectMenu = FindObjectOfType<LoadSelectMenuController>();
    }

    public void Initialize(string saveName)
    {
        SaveName = saveName;
        if (SaveNameText)
        {
            SaveNameText.text = SaveName;
        }
    }

    public void SelectSave()
    {
        loadSelectMenu.LoadScene(SaveName);
    }
}
