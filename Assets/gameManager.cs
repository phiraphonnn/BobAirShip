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
    
    public float distanceTraveled = 0f;
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
        float distanceThisFrame = speed / 3600f * Time.deltaTime; 
        
        // 3600 คือ วินาทีทั้้งหมดของ ชม จะแปลงเป็น 60 ก็ได้ จะ เวลาในเกมก็จะเท่ากับ 1 ชม = 60 วิ 
        distanceTraveled += distanceThisFrame;
        //Debug.Log("Distance traveled: " + distanceTraveled + " km");
    }

    public void caculateDistandtoScore()
    {
        scorePoint += distanceTraveled * 2 * 0.5f;

    }
    
    public void Normal()
    {
        Debug.Log("start");
        Acceleration = Force;
        FinalSpeed = Acceleration;
        
    }
    
    public void TextUpdate()
    {
        string formatteddistanceTraveled = distanceTraveled.ToString("0.00");
        DistanceText.text = "" + formatteddistanceTraveled + " km / " + distanceToTravel + " km";

        PlayerHpText.text = "" + Player.hpPlayer;
        ScoreText.text = scorePoint.ToString("0");

    }
    
    
    
    
}
