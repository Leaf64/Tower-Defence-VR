using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallShoot : MonoBehaviour {

    public GameObject rocket;
    public float deltaTime = 3f;
    float T = 1f;
    public Transform ShootPoint;
    public float predkosc;
    public Animator anim;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKey(KeyCode.Mouse0) || Input.touches.Length > 0 || Input.GetKey(KeyCode.S)) && T >= deltaTime && ParabolicPath.isTeleporting == false)
        {
            T = 0;
            Shoot();
        }
        T += Time.deltaTime;
    }
    void Shoot()
    {
        anim.SetTrigger("Shoot");
        GameObject Rocket = (GameObject)Instantiate(rocket, ShootPoint.position, ShootPoint.rotation);
        Rocket.GetComponent<Rigidbody>().AddForce(ShootPoint.transform.forward * predkosc, ForceMode.Impulse);
    }
}
