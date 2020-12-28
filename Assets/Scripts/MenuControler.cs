using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    public void BackPressed()
    {
        SceneManager.LoadScene("Menu");
    }

    public void McLevelPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void TfLevelPressed()
    {
        SceneManager.LoadScene(7);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
