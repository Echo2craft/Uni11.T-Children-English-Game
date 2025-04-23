using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu1 : MonoBehaviour
{
    /// Goes back to main menu
    public void GoToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
