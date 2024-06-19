using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("Ganti Scene");
        SceneManager.LoadScene("Gameplay");
    }

    public void OnApplicationQuit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
