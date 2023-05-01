using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class playerControl : MonoBehaviour
{
    [Header("playerHP setting")]
    public int hpPlayer;
    public int hpPlayerMax;

    [Header("Gun&Bullet setting")]
    public GameObject selectedGun;

    [SerializeField] private GameObject playerGun1;

    [SerializeField] private GameObject playerGun2;

    [SerializeField] private TMP_Text cooldownText;
    public float cooldownTime = 10f;
    private float cooldownTimer = 0;
    
    [SerializeField] private TMP_Text cooldownText2;

    [SerializeField] private GameObject target;
    
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject BlackHolebullet;
    [SerializeField] private GameObject harpoon;
    
    [Header("Movement setting")]
    public float speed = 3.0f;
    public float sliderSpeed = 0.1f;
    public Slider slider;


    public void Start()
    {
        hpPlayer = hpPlayerMax;
        selectedGun = bullet;
    }

   public void Update()
    {
     playerMove();

     selectedGun = CheckSelectedGun();
     RotatePalyerGun(selectedGun);
     
     if (cooldownTimer > 0)
     {
         cooldownTimer -= Time.deltaTime;
         cooldownText.text = cooldownTimer.ToString("0");
     }
     else
     {
         cooldownText.text = "";
     }
     
     if (Input.GetMouseButtonDown(0))
     {
         if (selectedGun == harpoon && cooldownTimer <= 0)
         {
             cooldownTimer = cooldownTime;
             LaunchProjectile(selectedGun);
         }
         if (selectedGun == BlackHolebullet && cooldownTimer <= 0)
         {
             cooldownTimer = cooldownTime;
             LaunchProjectile(selectedGun);
         }

         if (selectedGun == bullet)
         {
             LaunchProjectile(selectedGun);
         }
         
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
            EnemyBulletBehavior bullet = collision.GetComponent<EnemyBulletBehavior>();
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

    private GameObject CheckSelectedGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return bullet;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return BlackHolebullet;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return harpoon;
        }

        return selectedGun;
    }
    

    private void RotatePalyerGun(GameObject SelectedGun)
    {
        Transform gunSelected = null;
        if (SelectedGun == bullet)
        {
            gunSelected = playerGun1.transform;
        }
        if (SelectedGun == BlackHolebullet)
        {
            gunSelected = playerGun1.transform;
        }
        
        
        else if (SelectedGun == harpoon)
        {
            gunSelected = playerGun2.transform;
        }
        
        target.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10f));
        Vector2 direction = target.transform.position - gunSelected.position;
        gunSelected.right = direction;
        
    }
    
    
    private void LaunchProjectile(GameObject projectile)
    {
        Transform gunSelected = null;
        if (projectile == bullet)
        {
            gunSelected = playerGun1.transform;
        }
        if (projectile == BlackHolebullet)
        {
            gunSelected = playerGun1.transform;
        }
        else if (projectile == harpoon)
        {
            gunSelected = playerGun2.transform;
        }
        
        GameObject newProjectile = Instantiate(projectile, gunSelected.position, Quaternion.identity);
    
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Set the z-coordinate to 0 to ensure the projectile spawns at the same depth as the player
    
        // Rotate the projectile to face the mouse position
        Vector2 direction = mousePosition - newProjectile.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        
        //Bullet
        // Add force to the projectile to shoot it towards the mouse position
        if (projectile == bullet)
        {
            rb.velocity = CaculateProjectVelocity(gunSelected.position, target.transform.position, 1f);
        }
        
        if (projectile == BlackHolebullet)
        {
            rb.velocity = CaculateProjectVelocity(gunSelected.position, target.transform.position, 1f);
        }

        
        if (projectile == harpoon)
        {
           rb.AddForce(direction*400,ForceMode2D.Force);
           cooldownText.text = "10";
        }
        
    }
    

    
    
    private Vector2 CaculateProjectVelocity(Vector2 origin,Vector2 target,float time)
    {
        Vector2 distanc = target = target - origin;

        float disX = distanc.x;
        float disY = distanc.y;

        float velocityX = disX / time;
        float velocityY = disY / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        Vector2 result = new Vector2(velocityX, velocityY);
        
        return result;
    }

    #endregion
}
