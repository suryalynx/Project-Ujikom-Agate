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
    public GameObject mainCamera;
    public GameObject gameOverCamera;
    public TextMeshProUGUI scoreTextGameOver;

    [Header("Object Player")]
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;


    [Header("Animator")]
    public Animator animator;

    private int score = 100;
    private bool isGameOver = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOver.SetActive(false);
        mainCamera.SetActive(true);
        gameOverCamera.SetActive(false);

        SFXManager.instance.PlayBGM();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateScoreUI();
    }

    void Update()
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
    }

    void GameOver()
    {
        isGameOver = true;
        SFXManager.instance.PlaySFX("SFXGameOver");
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetTrigger("GameOver");
        mainCamera.SetActive(false);
        gameOverCamera.SetActive(true);
        gameOver.SetActive(true);

        scoreTextGameOver.text = score.ToString();
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
}
