using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Play()
    {

        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
