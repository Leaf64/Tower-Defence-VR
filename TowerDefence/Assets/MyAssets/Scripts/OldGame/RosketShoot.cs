using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosketShoot : MonoBehaviour {

    public GameObject rocket;
    public float deltaTime = 3f;
    float T = 1f;
    public Transform ShootPoint;
    public float predkosc;
    public Animator anim;
    public GameObject Player;
    RaycastHit hit;
    GameObject tower;
    public Color towerColor;
    bool onTower = false;
    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, 100))
        {

            if (hit.transform.gameObject.tag == "Tower")
            {
                if (onTower == false)
                {
                    tower = hit.transform.gameObject;
                    onTower = true;
                }
                hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;

            }
            else
            {
                if (tower && onTower == true)
                {
                    tower.GetComponent<Renderer>().material.color = towerColor;
                    onTower = false;
                }
            }
        }
        else
        {
            if (tower && onTower == true)
            {
                tower.GetComponent<Renderer>().material.color = towerColor;
                onTower = false;
            }
        }
        if ((Input.GetKey(KeyCode.Mouse0) || Input.touches.Length > 0 || Input.GetKey(KeyCode.S)) && T >= deltaTime)
        {
            T = 0;
            Shoot();
        }
        T += Time.deltaTime;


    }
    void Shoot()
    {
        if (Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, 100))
        {

            if (hit.transform.gameObject.tag == "Tower")
            {
                if (tower && onTower == true)
                {
                    tower.GetComponent<Renderer>().material.color = towerColor;
                    onTower = false;
                }
                Player.transform.position = hit.transform.gameObject.GetComponent<Tower>().trans.position;
                Player.transform.rotation = hit.transform.gameObject.GetComponent<Tower>().trans.rotation;
            }
            else
            {
                anim.SetTrigger("Shoot");
                GameObject Rocket = (GameObject)Instantiate(rocket, ShootPoint.position + ShootPoint.forward, ShootPoint.rotation);
                Rocket.GetComponent<Rigidbody>().AddForce(ShootPoint.transform.forward * predkosc, ForceMode.Impulse);
            }
        }
        else
        {
            anim.SetTrigger("Shoot");
            GameObject Rocket = (GameObject)Instantiate(rocket, ShootPoint.position + ShootPoint.forward, ShootPoint.rotation);
            Rocket.GetComponent<Rigidbody>().AddForce(ShootPoint.transform.forward * predkosc, ForceMode.Impulse);
        }
    }

}
