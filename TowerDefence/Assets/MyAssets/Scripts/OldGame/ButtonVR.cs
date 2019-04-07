using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVR : MonoBehaviour {

    public int numberScene = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangeScene()
    {
        Application.LoadLevel(numberScene);
    }

}
