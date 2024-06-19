using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI ScoreText;
    public GameObject gameOver;
    public GameObject mainCamera;
    public GameObject gameOverCamera;
    public float countDown = 60f;

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
        animator.SetBool("GameOver",true);
        mainCamera.SetActive(false);
        gameOverCamera.SetActive(true);
        gameOver.SetActive(true);
    }
}
