using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonBehavior : BulletBehavior
{
    private void Start()
    {
        Invoke("DestroyObject", timetoDestroy);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("harboon");
        }
        
    }
    
    
    
}
