using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cocaine : MonoBehaviour
{
    public float speed = 5f;

    public int Score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        
    }
    
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.current.scorePoint += Score;
            Destroy(gameObject);
        }
        
    }
}
