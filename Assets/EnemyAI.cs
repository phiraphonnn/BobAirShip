using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //hp
    public int hpEnemy;
    public int hpEnemyMax;
    
    public WaveManager waveManager;
    //moveAi
    public float speed;

    public float stoppingDistance;

    private float retreatDistance;

    private float randompingpong;
    

    public float  Score;

    public GameObject EnemyBullet;
    
    public Transform player;
    
    public Transform GunEnemyPosition;
    
    
    public float GuncooldownTime = 3.0f; // The amount of time the cooldown will last
    private float cooldownTimer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        randompingpong = Random.Range(3, 8);
        retreatDistance = stoppingDistance - 1;
        hpEnemy = hpEnemyMax;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    
      //  stoppingDistance = Random.Range(7f, 15f);
      //  retreatDistance = Random.Range(7f, 15f);
    }

    // Update is called once per frame
   public void Update()
    {
        AiMove();
        EnemyShoot();
    }

   void AiMove()
   {
       Vector2 targetPosition = player.position + new Vector3(0f, Mathf.Sin(Time.time) * 2f, 0f); // add a sine wave to the y-position

       if (Vector2.Distance(transform.position, targetPosition) > stoppingDistance)
       {
           transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
       }
       else if (Vector2.Distance(transform.position, targetPosition) < retreatDistance)
       {
           transform.position = Vector2.MoveTowards(transform.position, targetPosition, -speed * Time.deltaTime);
       }
       transform.position = new Vector2(transform.position.x, Mathf.PingPong(Time.time, randompingpong) - 1f);
   }
   
   
  public void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.CompareTag("Bullet"))
       {
           BulletBehavior bullet = collision.GetComponent<BulletBehavior>();
           HitFlashEff flashEff = gameObject.GetComponent<HitFlashEff>();
           flashEff.Flash();
           hpEnemy -= bullet.damage;
           Debug.Log("Enemy hit by bullet for " + bullet.damage + " damage. HP: " + hpEnemy);

           if (hpEnemy <= 0)
           {
               Die();
           }
       }
   }

   void Die()
   {
       gameManager.current.scorePoint += Score;
       Debug.Log("Enemy died.");
       waveManager.EnemyDefeated();
       Destroy(gameObject);
   }

   public void EnemyShoot()
   {
       if (cooldownTimer > 0)
       {
           cooldownTimer -= Time.deltaTime; // Reduce the remaining time on the cooldown by the time since the last frame
       }
       else
       {
           // The cooldown has ended, so perform the action that was on cooldown
         
           LaunchProjectile(EnemyBullet);
           // Start the cooldown again
           cooldownTimer = GuncooldownTime;
       }
      
   }
   private void LaunchProjectile(GameObject projectile)
   {

       GameObject newProjectile = Instantiate(projectile, GunEnemyPosition.position, Quaternion.identity);
    
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
     
       
       Vector3 playerPosition = player.transform.position +  new Vector3(Random.Range(-5,5), Random.Range(-5,5), 0f);
           rb.velocity = CaculateProjectVelocity(GunEnemyPosition.position, playerPosition, 1f);
       
       
        
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
}