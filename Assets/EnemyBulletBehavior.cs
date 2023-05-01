using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public int damage;
    public float timetoDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", timetoDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
            if (collision.CompareTag("Player"))
            {
        
                Destroy(gameObject);
            }
        

    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
