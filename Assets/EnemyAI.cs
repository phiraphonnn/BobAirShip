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
    
    public float cooldownTime;

    public float  Score;
    
    public Transform player;
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
   
}