using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    
    
    void OnBecameInvisible() {
            Destroy(gameObject);
    }
    

}
