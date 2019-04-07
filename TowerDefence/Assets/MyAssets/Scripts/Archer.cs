using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour {

    public Transform target;
    public float destinationReachedTreshold = 20f;
    float distanceToTarget;
    public Animator anim;
    public float HP = 100f;
    public float deltaTime = 1f;
    float time = 0;
    public GameObject strzala;
    public Transform ShootPoint;
    public float predkosc;
    public float randomize = 2f;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        transform.rotation = Quaternion.LookRotation(new Vector3((target.position - transform.position).x, 0, (target.position - transform.position).z));
        if (HP <= 0)
        {
            Dead();
        }
        distanceToTarget = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.position.x, target.position.z));
        if (distanceToTarget < destinationReachedTreshold)
        {
            Attack();
        }
        else
        {
            time = 0;
            anim.SetBool("isAttack", false);
        }
    }
    void Attack()
    {
        time += Time.deltaTime;
        anim.SetBool("isAttack", true);
        if (time >= deltaTime)
        {
            Shoot();
            time = 0;
        }
    }
    void Dead()
    {
        anim.SetTrigger("isDead");
        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Animator>(), 5f);
        Destroy(this);
    }
    public void TakeDamage(float damage)
    {
        HP -= damage;
    }
    void Shoot()
    {
        Vector3 targetPos = target.position + new Vector3(Random.RandomRange(-randomize, randomize), Random.RandomRange(-randomize, randomize), Random.RandomRange(-randomize, randomize));
        GameObject Strzala = (GameObject)Instantiate(strzala, ShootPoint.position, Quaternion.LookRotation(targetPos - ShootPoint.position));
        Strzala.GetComponent<Rigidbody>().AddForce((targetPos - ShootPoint.position).normalized * predkosc, ForceMode.Impulse);
    }
}
