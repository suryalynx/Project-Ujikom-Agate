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
    public TextMeshProUGUI ScoreText;
    public float countDown = 60f;

    [Header("Game Over Settings")]
    public GameObject gameOver;
    public GameObject mainCamera;
    public GameObject gameOverCamera;
    [Header("Object Player")]
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    

    [Header("SFX")]
    public AudioClip[] sfx;
    public Animator animator;

    private int score = 0;
    private bool isGameOver = false;

    private void Awake(){
        instance = this;
    }

    private void Start()
    {
        gameOver.SetActive(false);
        mainCamera.SetActive(true);
        gameOverCamera.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update(){
        if(!isGameOver){
            countDown -= Time.deltaTime;
            countDownText.text = Mathf.Ceil(countDown).ToString();

            if(countDown <= 0){
                GameOver();
            }
        }
    }

    void GameOver(){
        isGameOver = true;
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        animator.SetTrigger("GameOver");
        mainCamera.SetActive(false);
        gameOverCamera.SetActive(true);
        gameOver.SetActive(true);
    }
}
