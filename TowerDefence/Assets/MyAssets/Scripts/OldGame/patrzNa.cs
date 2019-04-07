using UnityEngine;
using System.Collections;

public class patrzNa : MonoBehaviour {

	private Transform target;
	private GameObject player;
	private Transform myTransform;
	// Use this for initialization
	void Start () {

	}

	void Awake()
	{
		myTransform = transform;

	}

	// Update is called once per frame
	void Update () {
		player = GameObject.FindWithTag ("Player");
		if (player != null) {
			target = player.transform;
				myTransform.rotation = Quaternion.LookRotation (new Vector3 (target.position.x, 0, target.position.z) - new Vector3 (myTransform.position.x, 0, myTransform.position.z));
		}
	}
}
