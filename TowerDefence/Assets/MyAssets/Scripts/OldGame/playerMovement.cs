using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {
	
	UnityEngine.AI.NavMeshAgent agent;
	
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			// ScreenPointToRay() takes a location on the screen
			// and returns a ray perpendicular to the viewport
			// starting from that location
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			// Note that "11" represents the number of the "ground"
			// layer in my project. It might be different in yours!
			LayerMask mask = 1 << 11;
			
			// Cast the ray and look for a collision
			if (Physics.Raycast(ray, out hit, 200, mask)) {
				// If we detect a collision with the ground, 
				// tell the agent to move to that location
				agent.destination = hit.point;
			}
		}
	}
}