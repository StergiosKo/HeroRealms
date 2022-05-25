using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsWindow;
    private bool windowOpened = false;

    public void WindowOpenClose()
    {
        settingsWindow.SetActive(windowOpened);
    }

    public void SettingsPressed()
    {
        windowOpened = !windowOpened;
        WindowOpenClose();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape")) SettingsPressed();
    }
}
