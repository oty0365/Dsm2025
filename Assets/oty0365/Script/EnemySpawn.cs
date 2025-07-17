using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class EnemyUnlockRate
{
    public GameObject enemy;
    public int stage;
}

public class EnemySpawn : HalfSingleMono<EnemySpawn>
{
    [Header("Spawn Settings")]
    public float initialSpawnCoolDown = 3f;
    public float minSpawnCoolDown = 0.2f;
    public float difficultyIncreaseRate = 1.1f;
    public float difficultyIncreaseInterval = 5f;
    
    [SerializeField]
    private float distance = 5f;
    private GameObject player;
    public List<EnemyUnlockRate> unlockRate;
    private List<GameObject> enemy = new();
    
    private float currentSpawnCoolDown;
    
    void Start()
    {
        player = PlayerStatus.Instance.gameObject;
        currentSpawnCoolDown = initialSpawnCoolDown;
        StartCoroutine(SpawnEnemy());
        StartCoroutine(IncreaseDifficulty());
    }
    
    public void TryUnlockEnemies(int currentScore)
    {
        List<EnemyUnlockRate> unlocked = new List<EnemyUnlockRate>();
        foreach (var data in unlockRate)
        {
            if (data.stage <= currentScore)
            {
                if (!enemy.Contains(data.enemy))
                {
                    enemy.Add(data.enemy);
                }
                unlocked.Add(data);
            }
        }
        
        foreach (var data in unlocked)
        {
            unlockRate.Remove(data);
        }
    }
    
    IEnumerator IncreaseDifficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultyIncreaseInterval);
            
            currentSpawnCoolDown = Mathf.Max(
                currentSpawnCoolDown * difficultyIncreaseRate, 
                minSpawnCoolDown
            );
        }
    }
    
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int ran = Random.Range(0, 360);
            float x = Mathf.Cos(ran * Mathf.Deg2Rad) * distance;
            float y = Mathf.Sin(ran * Mathf.Deg2Rad) * distance;
            Vector3 pos = player.transform.position + new Vector3(x, y, 0);
            
            if (enemy.Count > 0)
            {
                ObjectPooler.Instance.Get(enemy[Random.Range(0, enemy.Count)], pos, new Vector3(0, 0, 0));
            }
            
            yield return new WaitForSeconds(currentSpawnCoolDown);
        }
    }
}
