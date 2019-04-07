using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGrab : MonoBehaviour {


    public Material enabledMaterial;
    public Material disabledMaterial;
    GameObject gunTrigger;
    bool isEnabled = false;
    int enableds = 0;
    public GameObject startGun;
    GameObject actualGun;
    // Use this for initialization
    void Start () {
        actualGun = Instantiate(startGun, transform.position, transform.rotation, transform);
	}
	
	// Update is called once per frame
	void Update () {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach(Collider col in hitColliders)
        {
            if(col.tag == "gun" && isEnabled == false )
            {
                gunTrigger = col.gameObject;
                col.gameObject.GetComponent<Renderer>().material = enabledMaterial;
                isEnabled = true;
            }
        }
        enableds = 0;
        foreach (Collider col in hitColliders)
        {
            if (col.gameObject == gunTrigger)
            {
                enableds++;
            }
        }
        if (enableds > 0)
        {
            isEnabled = true;
        }
        else
        {
            isEnabled = false;
        }
        if(isEnabled == false)
        {
            gunTrigger.GetComponent<Renderer>().material = disabledMaterial;
        }
        if(isEnabled && (Input.GetKeyDown(KeyCode.Mouse0) || Input.touches.Length > 0 || Input.GetKeyDown(KeyCode.S)))
        {
            Destroy(actualGun);
            actualGun = Instantiate(gunTrigger.GetComponent<Gun>().gun, transform.position, transform.rotation, transform);
        }


    }
    
}
