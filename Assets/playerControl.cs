using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class playerControl : MonoBehaviour
{
    //player hp
    public int hpPlayer;
    public int hpPlayerMax;
    
    //playerBUllet
    private int selectedBullet;

    [SerializeField] private GameObject playerGun;
    [SerializeField] private GameObject target;
    
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject harpoon;
    
    
    //player move
    public float speed = 3.0f;
    public float sliderSpeed = 0.1f;
    public Slider slider;


    public void Start()
    {
        hpPlayer = hpPlayerMax;
    }

   public void Update()
    {
     playerMove();

     target.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10f));
     Vector2 direction = target.transform.position - playerGun.transform.position;
     playerGun.transform.right = direction;
     
     if (Input.GetMouseButtonDown(0))
     {
         AimAndFire();
     }
    
    }

    public void playerMove()
    {
        float sliderValue = slider.value;
        float speed = Mathf.Abs(sliderValue * 3); // Get the absolute value of the slider value as the speed

        if (Input.GetKey(KeyCode.W)) // Increase slider value
        {
            sliderValue += sliderSpeed * Time.deltaTime;
            sliderValue = Mathf.Clamp(sliderValue, -1f, 1f); // Keep slider value between -1 and 1// Keep slider value between 0 and 1
            slider.value = sliderValue; // Update slider value
        }

        if (Input.GetKey(KeyCode.S)) // Decrease slider value
        {
            sliderValue -= sliderSpeed * Time.deltaTime;
            sliderValue = Mathf.Clamp(sliderValue, -1f, 1f); // Keep slider value between -1 and 1
            slider.value = sliderValue; // Update slider value
        }

        
        if (sliderValue > 0) // Move up
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else // Move down
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }
    }

    public  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet")) //ต้องเปลี่ยนเป็น EnemyBullet or Enemy 
        {
            BulletBehavior bullet = collision.GetComponent<BulletBehavior>();
            HitFlashEff flashEff = gameObject.GetComponent<HitFlashEff>();
            flashEff.Flash();
            hpPlayer -= bullet.damage;
            Debug.Log("Enemy hit by bullet for " + bullet.damage + " damage. HP: " + hpPlayer);

            if (hpPlayer <= 0)
            {
                Die();
            }

            Destroy(collision.gameObject);
        }
    }

    void Die()
    {
        Debug.Log("player died.");
        Destroy(gameObject);
    }

    #region GunPlay

    private void  AimAndFire()
    {
        //เว้นไว้ เปลี่ยนปืน
        
        //ยิง
        LaunchProjectile(bullet);
    }

    // ทำมาก่อนอยากลองยิงได้
    private void LaunchProjectile(GameObject projectile)
    {
        GameObject newProjectile = Instantiate(projectile, playerGun.transform.position, Quaternion.identity);
    
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Set the z-coordinate to 0 to ensure the projectile spawns at the same depth as the player
    
        // Rotate the projectile to face the mouse position
        Vector2 direction = mousePosition - newProjectile.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    
        // Add force to the projectile to shoot it towards the mouse position
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction.normalized * 500f);
    }

    #endregion
}
