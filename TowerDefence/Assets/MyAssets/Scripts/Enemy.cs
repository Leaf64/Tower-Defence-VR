using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    NavMeshAgent navAgent;
    public Transform target;
    public float destinationReachedTreshold = 1;
    public float maxDistance= 10;
    float distanceToTarget;
    public Animator anim;
    public LayerMask layer;//all
    public Material decalMaterial; //decal
    public float maxSize = 0.8f;//decal
    public float minSize = 1f;//decal
    public float destroyTime = 10f;//decal
    public float destroySpeed = 0.1f;//decal
    public float HP = 100f;
    public float deltaTime = 1f;
    float time = 0;
    // Use this for initialization
    void Start () {
        navAgent = GetComponent<NavMeshAgent>();
    }
	void LateUpdate()
    {
        if (distanceToTarget < destinationReachedTreshold)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3((target.position - transform.position).x,0, (target.position - transform.position).z));
        }
    }
	// Update is called once per frame
	void Update () {
        // Debug.Log(distanceToTarget);
        if (HP <= 0)
        {
            Dead();
        }
        distanceToTarget = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.position.x, target.position.z));
        if (distanceToTarget <= maxDistance)
        {
            anim.SetBool("isWalking", true);
            navAgent.SetDestination(target.position);
        }
        else
        {
            anim.SetBool("isWalking", false);
            navAgent.SetDestination(transform.position);
        }
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
            target.GetComponent<PlayerHealth>().TakeDamage(20f);
            time = 0;
        }
    }
    void Dead()
    {
        anim.SetTrigger("isDead");
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Animator>(),5f);
        Destroy(this);
    }
    public void TakeDamage(float damage)
    {
        HP -= damage;
        Splash();
    }

    public void Splash()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10, layer))
        {
            STB.ADAOPS.GenericMeshDecal actualDecal = STB.ADAOPS.DecalInGameManager.DECAL_INGAME_MANAGER.CreateNewMeshDecal(decalMaterial, hit.transform, hit.point, hit.normal, Random.Range(minSize, maxSize), new Vector2(0, 360), false, layer);//decal

            actualDecal.SetDestroyable(true, destroyTime, destroySpeed);//decal
                                                                        //      InkCanvas canvas = hit.transform.gameObject.GetComponent<InkCanvas>();//ink
                                                                        //      if (canvas != null)//ink
                                                                        //          canvas.Paint(brush, hit.point,cam);//ink
        }
    }

}
