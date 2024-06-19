using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text countDownText;
    public GameObject gameOver;
    private float countDown = 60f;
    private bool isGameOver = false;

    private void Start()
    {
        gameOver.SetActive(false);
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
        gameOver.SetActive(true);
    }
}
