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
    public float spawnCoolDown = 3f;
    [SerializeField]
    private float distance = 5f;
    private GameObject player;
    public List<EnemyUnlockRate> unlockRate;
    private List<GameObject> enemy = new();
    
    void Start()
    {
        player = PlayerStatus.Instance.gameObject;
        StartCoroutine(SpawnEnemy());
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

            yield return new WaitForSeconds(spawnCoolDown);
        }
    }

    

}
