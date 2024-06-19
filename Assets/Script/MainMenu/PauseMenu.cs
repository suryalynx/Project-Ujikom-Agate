using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] hideUI;
    private bool isPaused = false;
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Pause.performed += OnPause;
    }

    private void OnDisable()
    {
        inputActions.UI.Pause.performed -= OnPause;
        inputActions.UI.Disable();
    }
    private void Start()
    {
        hideUI[0].SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
            Debug.Log("pause");
        }
    }

    public void Resume()
    {
        hideUI[0].SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
        if (playerAttack != null)
        {
            playerAttack.enabled = true;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");

    }
    public void Pause()
    {
        hideUI[0].SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        if (playerAttack != null)
        {
            playerAttack.enabled = false;
        }
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
    }
}
