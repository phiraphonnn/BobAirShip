using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;

    public float stoppingDistance;

    public float retreatDistance;

    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
      //  stoppingDistance = Random.Range(7f, 15f);
      //  retreatDistance = Random.Range(7f, 15f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            
        }
        else if (Vector2.Distance(transform.position,player.position) > stoppingDistance && Vector2.Distance(transform.position,player.position) > stoppingDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position,player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
    }
}