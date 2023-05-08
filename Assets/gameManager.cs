using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public float Acceleration;
    public float FinalSpeed;
    public float Force;
    public float speed;
    
    public float distanceTraveled;
    public float distanceToTravel;
    public static gameManager current;

    public int scorePoint;
    
    [SerializeField] public playerControl Player;
    [SerializeField] public TMP_Text DistanceText;
    [SerializeField] public TMP_Text PlayerHpText;
    [SerializeField] public TMP_Text ScoreText;
    
    [SerializeField] public TMP_Text VictoryText;

    public int highScore;
    public TMP_Text highscoreText;
    
    private float lastScorePoint = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        scorePoint = 0;
        Victory(false);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Normal();

        if (Victory(false) == false)
        {
            caculateDistand();
        }

        TextUpdate();
        

        if (speed != FinalSpeed)
        {
                
            speed += Acceleration * Time.deltaTime;
            if (speed >= FinalSpeed)
            {
                speed = FinalSpeed;
            }
        }

        if (Player == null)
        {
            Victory(true,"LOSER");
        }
        
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        } 
    }

    public void caculateDistand()
    {
        float increaseRate = 1.66f;
        distanceTraveled += increaseRate * Time.deltaTime;
        
        caculateDistandtoScore();
        
        
    }

    public void caculateDistandtoScore()
    {
        if (distanceTraveled >= lastScorePoint + 5)
        {
            scorePoint += 100;
            lastScorePoint += 5;
        }
        
        
        
    }
    
    public void Normal()
    {
        Acceleration = Force;
        FinalSpeed = Acceleration;
    }
    
    public void TextUpdate()
    {
        float disntaceleft = distanceToTravel - distanceTraveled;
        string formatteddistanceTraveled = disntaceleft.ToString("0");
        DistanceText.text = "" + formatteddistanceTraveled + " km";

        PlayerHpText.text = "" + Player.hpPlayer;
        ScoreText.text = scorePoint.ToString("0");

        if (Mathf.Abs(distanceTraveled) >= 100)
        {
            Victory(true);
        }
    }

    public bool Victory(bool check)
    {
        if (check == true)
        {
            VictoryText.gameObject.SetActive(check);
            Time.timeScale = 0f;

            if (scorePoint > highScore)
            {
                highScore = scorePoint;
                highscoreText.text = "HIGH SCORE : " + highScore;
            }
            
            return true;
        }

        else
        {
            return false;
        }
    }

    public void Victory(bool check, string text)
    {
        if (check == true)
        {
            VictoryText.text = text;
            VictoryText.gameObject.SetActive(check);
        }
        
    }
    
    
}
