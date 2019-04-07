using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkEnabling : MonoBehaviour {

    float distanceToTarget;
    public Transform target;
    public float enableDistance = 10f;
    public Animator animFather;
    public Animator anim;
    public bool animEnabled = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        distanceToTarget = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.position.x, target.position.z));
        if (distanceToTarget <= enableDistance)
        {
            animFather.SetBool("Start", true);
            anim.SetBool("Start", true);
        }
        animFather.enabled = animEnabled;
    }
}
