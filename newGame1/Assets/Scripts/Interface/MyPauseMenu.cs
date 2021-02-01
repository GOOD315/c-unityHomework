using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool activePause = false;
    public bool Active { get => activePause; set => activePause = value; }
    MyAdvancedGUI myGUI;


    private void Start()
    {
        myGUI = gameObject.GetComponent<MyAdvancedGUI>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activePause = !activePause;
            pauseMenu.SetActive(activePause);
            myGUI.enabled = activePause;

            if (activePause)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }




    }
}
