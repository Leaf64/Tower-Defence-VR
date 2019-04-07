using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [Range(0,100)]
    public float HP = 100f;
    public Image[] image;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Image img in image)
        {
            img.color = new Color32(255,255,255,(byte)((float)(255f / 100f) * (100 - HP)));
        }
	}
    public void TakeDamage(float damage)
    {
        HP -= damage;
    }
}
