using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    NavMeshAgent navAgent;
    public Checkpoints[] checkpoints;
    public Transform[] targets;
    enemyMovement movement;
    float destinationReachedTreshold = 1;
     public int a;
     public int b;
     public float distanceToTarget;
    bool end;
    float distanceToEnd;
	// Use this for initialization
	void Start () {
        movement = GetComponent<enemyMovement>();
        navAgent = GetComponent<NavMeshAgent>();
        a = 0;
        b = Random.Range(0, checkpoints[0].transforms.Length);
        end = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (a<checkpoints.Length)
        {
        //    movement.player = checkpoints[a].transforms[b];
            navAgent.SetDestination(checkpoints[a].transforms[b].position);
            distanceToTarget = Vector2.Distance(new Vector2(transform.position.x,transform.position.z),new Vector2(checkpoints[a].transforms[b].position.x, checkpoints[a].transforms[b].position.z));
            if (distanceToTarget < destinationReachedTreshold)
            {
                a++;
                if (a < checkpoints.Length)
                {
                    b = Random.Range(0, checkpoints[a].transforms.Length);
                }
            }
        }
        if (a >= checkpoints.Length && end == false)
        {
            b = Random.Range(0, targets.Length);
           // movement.player = targets[b];
            navAgent.SetDestination(targets[b].position);
            end = true;
        }
        distanceToEnd = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(targets[b].position.x, targets[b].position.z));
        if(distanceToEnd < destinationReachedTreshold)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        Application.LoadLevel(2);
    }
}
