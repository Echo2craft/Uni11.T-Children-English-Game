using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPause = false;

    [SerializeField]
    private GameObject

            pauseMenuUI;
    //        optionMenu;

    //[SerializeField]




    //private GameObject mainMenu;
public void Resume()
    {
        EventSystem.current.SetSelectedGameObject(null);
        GameIsPause = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(false);       
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
        

    public void Quit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Return()
    {
        GameIsPause = false;
        Time.timeScale = 1f;
    }

    //public void Options()
    //{
    //    if (GameIsPause)
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;
    //        Time.timeScale = 0f;
    //        GameIsPause = true;

    //    }
    //    else
    //    {
    //        Resume();
    //        pauseMenuUI.SetActive(false);
    //        optionMenu.SetActive(false);
    //        mainMenu.SetActive(false);
    //    }
    //}
}
