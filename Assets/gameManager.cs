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

    public float scorePoint;
    
    [SerializeField] public playerControl Player;
    [SerializeField] public TMP_Text DistanceText;
    [SerializeField] public TMP_Text PlayerHpText;
    [SerializeField] public TMP_Text ScoreText;
    
    [SerializeField] public TMP_Text VictoryText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        scorePoint = 0;
        Victory(false);
    }

    // Update is called once per frame
    void Update()
    {
        Normal();
        caculateDistand();
        TextUpdate();
        caculateDistandtoScore();
        
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

    }

    public void caculateDistandtoScore()
    {
        if  (Mathf.Abs(distanceTraveled % 5) < 0.001f)
        {
            scorePoint += 100;
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

        if (Mathf.Abs(disntaceleft) < 0.001f)
        {
            Victory(true);
        }
    }

    public void Victory(bool check)
    {
        if (check == true)
        {
            VictoryText.gameObject.SetActive(check);
            Time.timeScale = 0f;
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
