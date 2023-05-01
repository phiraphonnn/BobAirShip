using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public bool isBlackhole;
    public bool isBulletBlackhole;
    public GameObject blackHolePrefab;
    public int damage;

    
    
    public float rotationSpeed = 100f;

    
    
    public float timetoDestroy;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isBlackhole)
        {
              if (collision.CompareTag("Enemy"))
                    {
                        if (isBulletBlackhole)
                        {
                            DestroyBlackHole();
                        }
                      
                        Destroy(gameObject);
                    }
        }
      
        
    }

    private void Start()
    {
        if (isBulletBlackhole)
        {
            Invoke("DestroyBlackHole", timetoDestroy);
        }
        else
        {
            Invoke("DestroyObject", timetoDestroy);
        }
        
    }

    private void Update()
    {
        if (isBlackhole)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
    
    void DestroyBlackHole()
    {
        // Instantiate the prefab at the same position and rotation as this object
        Instantiate(blackHolePrefab, transform.position, transform.rotation);
        // Destroy this object
        Destroy(gameObject);
    }
}
