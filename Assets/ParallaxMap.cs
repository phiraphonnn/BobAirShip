using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMap : MonoBehaviour
{
    private float length;
    private float startpos;
    public float parrallexEffect;
    public float speed;

    private SpriteRenderer spriteRenderer;
   // public Camera cam;
 //   public float scaleFactor = 1.0f;

   // public GameObject cam;

    // Start is called before the first frame update
    private void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public  void Update()
    {
        // Adjust the local scale of the transform based on the camera's orthographic size
      //  float scale = cam.orthographicSize * scaleFactor;
   //     transform.localScale = new Vector3(scale, scale, 1);

        // Adjust the size of the sprite renderer based on the new local scale
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y) / transform.localScale.x;


            speed = gameManager.current.speed * 0.2f;

            transform.position += Vector3.left * speed * Time.deltaTime * (1 - parrallexEffect);

            if (transform.position.x > length)
            {
                transform.position = new Vector3(transform.position.x - length, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -length)
            {
                transform.position = new Vector3(transform.position.x + length, transform.position.y, transform.position.z);
            }
        
    }
}