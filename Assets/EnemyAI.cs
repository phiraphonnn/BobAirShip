using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //hp
    public int hpEnemy;
    public int hpEnemyMax;
    
    //moveAi
    public float speed;

    public float stoppingDistance;

    private float retreatDistance;

    public float cooldownTime;
    
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
     
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
       transform.position = new Vector2(transform.position.x, Mathf.PingPong(Time.time, 4f) - 1f);
   }
   
   
  public  void OnTriggerEnter2D(Collider2D collision)
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

           Destroy(collision.gameObject);
       }
   }

   void Die()
   {
       Debug.Log("Enemy died.");
       Destroy(gameObject);
   }
   
}