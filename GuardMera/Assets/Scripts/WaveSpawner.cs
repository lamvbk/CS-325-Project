using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]

    public float healthScalePerWave = 0.15f; // 15% more HP per wave
    public static int enemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint;
    
    public float timeBetweenWaves = 5f;
    public float countDown = 1f;
    public float spawnTiming = 1f;
    
    private int waveIndex = 0;

    void Update()
    {
        if(enemiesAlive > 0)
        {
            return;
        }

        if(countDown <= 0)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }

        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
    }

     IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        foreach(EnemyGroup enemyGroup in wave.eG)
        {
            enemiesAlive += enemyGroup.eCount;
        }
        foreach(EnemyGroup eG in wave.eG)
        {
            if(wave.variedTimeBetweenGroups)
            {
                yield return new WaitForSeconds(eG.delay);
            }
            else
            {
                yield return new WaitForSeconds(wave.timeBetweenGroups);
            }
            if(eG.enemy != null)
            {
                for(int i = 0; i < eG.eCount; i++)
                {    
                    SpawnEnemy(eG.enemy, waveIndex);
                    yield return new WaitForSeconds(spawnTiming/eG.rate);
                }
            }
        }
        while(enemiesAlive != 0)
        {
            yield return null;
        }
        waveIndex++;
        if((waveIndex == waves.Length))
        {
            while(enemiesAlive != 0)
            {
                yield return null;
            }
            this.enabled = false;
        }

    }

    void SpawnEnemy(GameObject enemyPrefab, int currWaveIndex) // changed by mahad
    {
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = spawnedEnemy.GetComponent<Enemy>();
        if(enemy != null)
        {
            float scaledHealth = enemy.maxHealth * (1f + currWaveIndex * healthScalePerWave);
            enemy.maxHealth = Mathf.RoundToInt(scaledHealth);
            enemy.currentHealth = enemy.maxHealth;
        }
        //Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

}
