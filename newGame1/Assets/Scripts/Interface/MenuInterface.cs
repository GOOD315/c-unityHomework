using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuInterface : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Slider volSlider;
    [SerializeField] private InputField input;
    [SerializeField] private string playerName;


    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(false);

        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    public void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        GetName();
    }

    private void GetName()
    {
        playerName = input.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
    }
}
