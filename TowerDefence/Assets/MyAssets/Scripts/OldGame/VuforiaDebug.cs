using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuforiaDebug : MonoBehaviour {

    public GameObject vuforia;

	// Use this for initialization
	void Start () {
        vuforia.SetActive(false);
        StartCoroutine("Wait");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private IEnumerator Wait()
    {
            yield return new WaitForSeconds(0.1f);
        vuforia.SetActive(true);
    }
}
