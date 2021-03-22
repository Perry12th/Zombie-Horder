using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveData;

public class LoadSelectMenuController : MenuWidget
{
    [SerializeField]
    private RectTransform LoadItemPanel;

    [SerializeField]
    private GameObject ItemSlotPrefab;

    [SerializeField]
    private TMP_InputField SaveNameInputField;

    private const string SaveFileKey = "FileSaveData";
    private const string GameSceneName = "Game_City_Map";

    private void Start()
    {
        LoadItemPanel.DetachChildren();

        LoadFileList();
    }

    public void LoadFileList()
    {
        if (PlayerPrefs.HasKey(SaveFileKey))
        {
            GameDataList gameDataList = JsonUtility.FromJson<GameDataList>(PlayerPrefs.GetString(SaveFileKey));
            foreach (string saveName in gameDataList.SaveFilesNames)
            {
                RectTransform widget = Instantiate(ItemSlotPrefab).GetComponent<RectTransform>();
                widget.GetComponent<LoadItemWidget>().Initialize(saveName);
                widget.SetParent(LoadItemPanel);
            }
        }
        
    }

    public void LoadScene(string saveName)
    {
        GameManager.Instance.SelectedSaveName = saveName;
        SceneManager.LoadScene(GameSceneName);
    }

    public void SaveFileList()
    {
        if (PlayerPrefs.HasKey(SaveFileKey))
        {
            GameDataList gameDataList = JsonUtility.FromJson<GameDataList>(PlayerPrefs.GetString(SaveFileKey));
            gameDataList.SaveFilesNames.Add(SaveNameInputField.text);

            string JsonString = JsonUtility.ToJson(gameDataList);
            PlayerPrefs.SetString(SaveFileKey, JsonString);
        }

        else
        {
            GameDataList gameDataList = new GameDataList();
            gameDataList.SaveFilesNames.Add(SaveNameInputField.text);

            string JsonString = JsonUtility.ToJson(gameDataList);
            PlayerPrefs.SetString(SaveFileKey, JsonString);
        }
    }
}
