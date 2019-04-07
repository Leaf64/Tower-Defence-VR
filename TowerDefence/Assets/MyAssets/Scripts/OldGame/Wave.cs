using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    public EnemySpawner spawner;
    public GameObject[] EnemyPrefab;
    public float timeSpawnDelta;
    public int[] EnemyAmount;
    public int EnemyAmountSum = 10;
    public int[] side; 

    // Use this for initialization
    void Start ()
    {
        EnemyAmountSum = 0;
		foreach(int amount in EnemyAmount)
        {
            EnemyAmountSum += amount;
        }
        EnemyAmountSum = EnemyAmountSum * side.Length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlayWave(int Count)
    {
        if (EnemyAmount.Length > Count)
        {
            StartCoroutine(WaveWait(Count));
        }
    }


    IEnumerator WaveWait( int Count)
    {
        if(Count == 0)
        {
            yield return new WaitForSeconds(3f);
        }
        for(int i =0;i<side.Length;i++) {
            spawner.Wave(EnemyAmount[Count], EnemyPrefab[Count], 0, side[i]);
        }
        Count++;
        yield return new WaitForSeconds(timeSpawnDelta*(EnemyAmount[Count-1]));
        PlayWave(Count);
    }


}
