using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonBehavior : BulletBehavior
{
    

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("harboon");
            
            
        }
        
        
    }
}
