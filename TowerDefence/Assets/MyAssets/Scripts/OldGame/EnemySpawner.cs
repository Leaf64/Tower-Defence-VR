using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

  //  public GameObject EnemyPrefab;
    public float timeSpawnDelta;
  //  public int maxEnemy = 10;
    public CheckpointsContainer[] Checks;
    public Transform[] Targets;
    public Wave[] waves;
    int counter = 0;
    bool waveEnded = true;
    public int EnemyCounter = 0;
    public Transform[] sides;
    public TextMesh text;
	// Use this for initialization
	void Start () {
        if (waves[0])
        {
            waves[0].PlayWave(0);
          //  Wave(waves[0].EnemyAmount, waves[0].EnemyPrefab, 0);
            EnemyCounter += waves[0].EnemyAmountSum;
            counter++;
            waveEnded = false;
        }

	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Wave " + (counter).ToString();
        if(EnemyCounter <= 0)
        {
            waveEnded = true;
        }
        if (waveEnded && counter < waves.Length)
        {
            waves[counter].PlayWave(0);
          //  Wave(waves[counter].EnemyAmount, waves[counter].EnemyPrefab, 0);
            EnemyCounter += waves[counter].EnemyAmountSum;
            counter++;
            waveEnded = false;
        }
       if(counter >= waves.Length && waveEnded == true)
        {
            Application.LoadLevel(0);
        }
		
	}
    void Spawn(GameObject EnemyPref, int side) {
        GameObject Enemy = Instantiate(EnemyPref, sides[side].position, sides[side].rotation);
        Enemy.GetComponent<BloodSplash>().spawner = this;
        Enemy.GetComponent<EnemyAI>().checkpoints = Checks[side].checks;
        Enemy.GetComponent<EnemyAI>().targets = Targets;
    }

    public void Wave(int EnemyAmount, GameObject EnemyPref,int EnemyCount,int Side)
    {
        if (EnemyAmount > EnemyCount)
        {
            StartCoroutine(WaveWait(EnemyAmount, EnemyPref, EnemyCount,Side));
        }
    }


     IEnumerator WaveWait(int EnemyAmount,GameObject EnemyPref, int EnemyCount,int Side)
    {
        Spawn(EnemyPref,Side);
        EnemyCount++;
        yield return new WaitForSeconds(timeSpawnDelta);
        Wave(EnemyAmount, EnemyPref, EnemyCount,Side);
    }


}
