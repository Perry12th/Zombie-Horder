using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UnPauseGame();
    }

    public void PauseGame()
    {
        var pausables = FindObjectsOfType<MonoBehaviour>().OfType<IPausable>();
        foreach (IPausable pauseObject in pausables)
        {
            pauseObject.PauseGame();
        }

        Time.timeScale = 0;
        AppEvents.Invoke_OnMouseCursorEnable(true);
    }

    public void UnPauseGame()
    {

        var pausables = FindObjectsOfType<MonoBehaviour>().OfType<IPausable>();
        foreach (IPausable pauseObject in pausables)
        {
            pauseObject.UnPauseGame();
        }

        Time.timeScale = 1;
        AppEvents.Invoke_OnMouseCursorEnable(false);
    }

    private void OnDestroy()
    {
        UnPauseGame();
    }
}

interface IPausable
{
    void PauseGame();
    void UnPauseGame();
}

