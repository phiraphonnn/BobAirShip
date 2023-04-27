using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public float Acceleration;
    public float FinalSpeed;
    public float Force;
    public float speed;
    
    public float distanceTraveled = 0f; 
    
    public static gameManager current;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        caculateDistand();
        
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
        Debug.Log("Distance traveled: " + distanceTraveled + " km");
    }
    public void Normal()
    {
        Debug.Log("start");
        Acceleration = Force;
        FinalSpeed = Acceleration;
        
    }
    /*
    public void stopAirShip()
    {
        FinalSpeed = 0;
    }*/
}
