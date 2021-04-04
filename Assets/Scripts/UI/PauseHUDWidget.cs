using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHUDWidget : GameHUDWidget
{
    [SerializeField]
    string ExitMenuSceneName;

    public void ResumeGame()
    {
        PauseManager.Instance.UnPauseGame();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(ExitMenuSceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
