using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("UI Content")]
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI scoreText;
    public float countDown = 60f;

    [Header("Game Over Settings")]
    public GameObject gameOver;
    public Camera mainCamera;
    public Camera gameOverCamera;
    public float transitionDuration = 1.0f;
    public TextMeshProUGUI scoreTextGameOver;

    [Header("Object Player")]
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;


    [Header("Animator")]
    public Animator animator;

    private int score = 0;
    private bool isGameOver = false;
    private float timeElapsed;
    private bool isTransitioning;

    private Camera currentCamera;
    private Camera targetCamera;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOver.SetActive(false);
        mainCamera.enabled = true;
        gameOverCamera.enabled = false;

        currentCamera = mainCamera;
        targetCamera = gameOverCamera;

        SFXManager.instance.PlayBGM();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UpdateScoreUI();
    }

    void Update()
    {
        ShowGameOver();
    }

    public void ShowGameOver()
    {
        if (!isGameOver)
        {
            countDown -= Time.deltaTime;
            countDownText.text = Mathf.Ceil(countDown).ToString();

            if (countDown <= 0)
            {
                GameOver();
            }
        }
        if (isTransitioning)
        {
            HandleCameraTransition();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        EnemySpawn.instance.StopSpawning();
        SFXManager.instance.PlaySFX("SFXGameOver");
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetTrigger("GameOver");
        gameOver.SetActive(true);
        scoreTextGameOver.text = score.ToString();

        StartCameraTransition();
    }

    public void AddScore(int amount)
    {
        score += amount;

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }

    private void StartCameraTransition()
    {
        isTransitioning = true;
        timeElapsed = 0;

        currentCamera.enabled = true;
        targetCamera.enabled = true;
    }

    private void HandleCameraTransition()
    {
        timeElapsed += Time.deltaTime;
        float t = timeElapsed / transitionDuration;
        t = t * t * (3f - 2f * t); // Ease In Out formula

        currentCamera.transform.position = Vector3.Lerp(currentCamera.transform.position, targetCamera.transform.position, t);
        currentCamera.transform.rotation = Quaternion.Slerp(currentCamera.transform.rotation, targetCamera.transform.rotation, t);

        if (timeElapsed >= transitionDuration)
        {
            EndCameraTransition();
        }
    }

    private void EndCameraTransition()
    {
        isTransitioning = false;
        timeElapsed = 0;
        targetCamera.enabled = false;
    }
}
