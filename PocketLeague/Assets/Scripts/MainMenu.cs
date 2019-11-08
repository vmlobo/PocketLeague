using QuantumTek.SimpleMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public SM_TabWindow initialTab;


    private void Update()
    {
        if (isWaitingForInput && Input.anyKey)
        {
            isWaitingForInput = false;
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    AssignKey(vKey.ToString());
                }
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Called when user presses Settings button or the cross to leave settings
    // Opens settings menu and closes the menu containing quit, settings and play
    //
    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        settingsMenu.GetComponent<SM_TabGroup>().ChangeTab(initialTab);
        settingsMenu.GetComponent<SM_TabGroup>().Toggle(true);
        FillControls();
    }

    // Called when the user presses the cross to leave settings
    // Closes settings menu and opens the menu containing quit, settings and play
    //
    public void CloseSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    ///////// Variables for changeControl
    bool isWaitingForInput = false;
    string controlBeingChanged = "";

    // User wants to choose his own controls
    // 
    public void changeControl(string controlType)
    {
        controlBeingChanged = controlType;
        isWaitingForInput = true; // Enables the update to check which input is pressed next by the user
    }

   // After the user pressed the key, 
   //
    private void AssignKey(string keyChosen)
    {
        PlayerPrefs.SetString(controlBeingChanged, keyChosen);
        GameObject.Find(controlBeingChanged + "_text").GetComponent<TextMeshProUGUI>().text = keyChosen.ToUpper();
        CheckIfKeyIsUnderUse(controlBeingChanged, keyChosen);
    }

    private void FillControls()
    {
        GameObject.Find(Constants.forwardKey + "_text").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString(Constants.forwardKey, "").ToUpper();
        GameObject.Find(Constants.backKey + "_text").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString(Constants.backKey, "").ToUpper();
        GameObject.Find(Constants.rightKey + "_text").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString(Constants.rightKey, "").ToUpper();
        GameObject.Find(Constants.leftKey + "_text").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString(Constants.leftKey, "").ToUpper();
        GameObject.Find(Constants.jumpKey + "_text").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString(Constants.jumpKey, "").ToUpper();
        GameObject.Find(Constants.NitroKey + "_text").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString(Constants.NitroKey, "").ToUpper();
    }

    private void CheckIfKeyIsUnderUse(string controlKey, string key)
    {
        if (Constants.forwardKey != controlKey && PlayerPrefs.GetString(Constants.forwardKey, "").ToUpper() == key.ToUpper())
        {
            clearEntry(Constants.forwardKey);
        }
        if (Constants.backKey != controlKey && PlayerPrefs.GetString(Constants.backKey, "").ToUpper() == key.ToUpper())
        {
            clearEntry(Constants.backKey);
        }
        if (Constants.rightKey != controlKey && PlayerPrefs.GetString(Constants.rightKey, "").ToUpper() == key.ToUpper())
        {
            clearEntry(Constants.rightKey);
        }
        if (Constants.leftKey != controlKey && PlayerPrefs.GetString(Constants.leftKey, "").ToUpper() == key.ToUpper())
        {
            clearEntry(Constants.leftKey);
        }
        if (Constants.jumpKey != controlKey && PlayerPrefs.GetString(Constants.jumpKey, "").ToUpper() == key.ToUpper())
        {
            clearEntry(Constants.jumpKey);
        }
        if (Constants.NitroKey != controlKey && PlayerPrefs.GetString(Constants.NitroKey, "").ToUpper() == key.ToUpper())
        {
            clearEntry(Constants.NitroKey);
        }
    }

    private void clearEntry(string control)
    {
        PlayerPrefs.SetString(control, "");
        GameObject.Find(control + "_text").GetComponent<TextMeshProUGUI>().text = "";
    }
}
