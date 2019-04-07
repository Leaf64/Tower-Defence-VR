using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public float damage = 500f;
    public float radius = 1f;
    public GameObject partics;
    public bool isHitPlayer = false;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {

       
    }
    void OnCollisionEnter(Collision collision)
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider col in hitColliders)
        {
            if (col.gameObject.GetComponent<ButtonVR>())
            {
                col.gameObject.GetComponent<ButtonVR>().ChangeScene();
            }
            if (isHitPlayer)
            {
                if (col.gameObject.GetComponent<PlayerHealth>())
                {
                    col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                }
            }
            else
            {
                if (col.gameObject.GetComponent<Enemy>())
                {
                    col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                }
                if (col.gameObject.GetComponent<Archer>())
                {
                    col.gameObject.GetComponent<Archer>().TakeDamage(damage);
                }
            }
        }
        if (partics != null)
        {
            GameObject Particles = Instantiate(partics, transform.position, transform.rotation);
            Destroy(Particles, 3);
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
