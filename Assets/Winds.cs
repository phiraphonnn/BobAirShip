using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Winds : MonoBehaviour
{
    public float moveSpeed = 5f; // The speed at which the object moves left
    public float rotationSpeed = 100f; // The speed at which the object rotates
    private Vector3 direction = Vector3.left; // The direction in which the object moves
  
    public void Start()
    {
        int randomSign1 = Random.value > 0.5f ? 0 : 180;
        int randomSign2 = Random.value > 0.5f ? 0 : 180;
        float randomValue1 = randomSign1;
        float randomValue2 = randomSign2;

        Debug.Log("Random value 1: " + randomValue1);
        Debug.Log("Random value 2: " + randomValue2);
        transform.Rotate(new Vector3(0f, 0f, randomValue1));
    }

    public void Update()
    {
        // Move the object left
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // Apply a random rotation around the z-axis
      
    }
    void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
    
  
}
