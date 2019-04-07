using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCube : MonoBehaviour {

    public GameObject cube;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Following();
	}
    void Following()
    {
        transform.position = cube.transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(cube.transform.forward.x,0, cube.transform.forward.z));
    }
}
