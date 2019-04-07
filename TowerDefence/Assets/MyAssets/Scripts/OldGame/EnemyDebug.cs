using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyDebug : MonoBehaviour {

     NavMeshAgent[] agents;

	// Use this for initialization
	void Start () {
        agents = FindObjectsOfType<NavMeshAgent>();
        int a = 1;
        foreach(NavMeshAgent agent in agents)
        {
            agent.avoidancePriority = a;
            a++;
        }

    }
	
	// Update is called once per frame
	void Update () {

		
	}
}
