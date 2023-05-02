using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        scorePoint = 0;
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
        
    }

    public void caculateDistand()
    {
        float increaseRate = 1.66f; // (ระยะ100/เวลา60)*ในเวลา1
        distanceTraveled += increaseRate * Time.deltaTime;

    }

    public void caculateDistandtoScore()
    {
        if (distanceTraveled % 5 == 0)
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

    }
    
    
    
    
}
