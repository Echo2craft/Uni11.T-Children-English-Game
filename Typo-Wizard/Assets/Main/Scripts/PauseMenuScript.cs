using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    public void Show()
    {
        pauseMenuPanel.SetActive(true);
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1; // VERY IMPORTANT
        EventSystem.current.SetSelectedGameObject(null);
    }
}