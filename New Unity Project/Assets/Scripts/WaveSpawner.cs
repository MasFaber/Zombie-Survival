using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;

    }

    public Wave[] waves;
    public int nextWave = 0;

    public Transform[] SpawnPoints;

    public float timeBetweenWaves = 2f;
    private float waveCountDown;

    private float searchCountDown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    public GameObject[] jaap;
     void Start()
    {
        waveCountDown = timeBetweenWaves;

    }

     void Update()
    {
        if (state == SpawnState.WAITING) {
            //check if enemies aree still alive
            if (!EnemyisAlive())
            {
             
                WaveCompleted();
                
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //start spwaning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted() {
        Debug.Log("wave completed");

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves completed! looping...");
            //or start new scene or whatever when they are done.
        }
        else
        {
            nextWave++;
        }
     

    }

    bool EnemyisAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0)
        {
            
            jaap = GameObject.FindGameObjectsWithTag("Enemy");

            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                Debug.Log("Dead!");
                return false;
            }

            searchCountDown = 1f;
        }
        Debug.Log("Alive!");
        return true;
        

    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave" + _wave.name);
        state = SpawnState.SPAWNING;
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }
        //spawn
        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
                Debug.Log("Spawning enemy" + _enemy.name);
        Transform _sp = SpawnPoints[Random.Range(0,SpawnPoints.Length)];

        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

}
