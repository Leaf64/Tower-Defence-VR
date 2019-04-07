using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VignetteControl : MonoBehaviour {

	public GameObject vignette;
	public float brightLvl;
	public float healingMultiplier;
	public float damageAmount;
	//public bool test;
	public bool gameOver;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//if (test) 
		//{
		//	Activate ();
		//	test = false;
		//}

		vignette.GetComponent<Image>().color = new Color32(255,255,225, (byte) brightLvl);

		brightLvl -= Time.deltaTime*healingMultiplier;

		if (brightLvl <= 0) 
		{
			brightLvl = 0;
		}

		if (brightLvl >= 255) 
		{
			gameOver = true;
		}

	}

	void Activate ()
	{

		brightLvl = brightLvl + damageAmount;

	}
}
