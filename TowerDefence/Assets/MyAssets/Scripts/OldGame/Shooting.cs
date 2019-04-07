using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
    public Transform ShootPoint;
    public GameObject particles;
    public LayerMask EffectedLayers;
    RaycastHit hit;
    public GameObject Player;
    public Animator anim;
    // public LayerMask EnemyLayer;
    public float damage = 50f;

    public float deltaTime = 0.25f;
    float T = 1f;

    GameObject tower;
    public Color towerColor;
    bool onTower = false;
    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(ShootPoint.position, ShootPoint.forward * 100f);
        if (Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, 100, EffectedLayers))
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
        if ((Input.GetKey(KeyCode.Mouse0) || Input.touches.Length > 0 || Input.GetKey(KeyCode.S))&& T >= deltaTime)
        {
            T = 0;
            Shoot();
        }
        T += Time.deltaTime;
    }
    void Shoot()
    {
        anim.SetTrigger("Shoot");
        
        if (Physics.Raycast(ShootPoint.position, ShootPoint.forward, out hit, 100,EffectedLayers))
        {
            
            if (hit.transform.gameObject.layer == 10)
            {
                
                hit.transform.gameObject.GetComponent<BloodSplash>().TakeDamage(damage);
                GameObject Partic = Instantiate(particles, hit.point, Quaternion.LookRotation(hit.normal));
                Partic.GetComponent<ParticleSystem>().startColor = Color.red;
                Destroy(Partic, 2);
            }
            else
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
                    if (hit.transform.gameObject.tag == "Button")
                    {
                        Application.LoadLevel(hit.transform.gameObject.GetComponent<ButtonVR>().numberScene);
                    }
                    else
                    {
                        GameObject Partic = Instantiate(particles, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(Partic, 2);
                    }
                }
            }
        }
    }
}
