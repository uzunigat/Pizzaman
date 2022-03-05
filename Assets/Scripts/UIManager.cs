using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text titleLabel;
    public Text scoreLabel;

    public static UIManager sharedInstance;
    public int totalScore;

    private void Awake()
    {
        if (sharedInstance == null) sharedInstance = this;
        totalScore = 0;

    }

    private void Update()
    {
        if(GameManager.sharedInstance.gamePaused || !GameManager.sharedInstance.gameStarted)
        {

            titleLabel.enabled = false;

        }
    }

    public void AddPoints(int points) {

        this.totalScore += points;
        scoreLabel.text = "Score: " + totalScore; 

    }

}
