using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public float spawnCoolDown = 3f;
    [SerializeField]
    private float distance = 5f;
    private GameObject player;
    public List<GameObject> enemy;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnEnemy());
    }

    

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int ran = Random.Range(0, 360);
            float x = Mathf.Cos(ran * Mathf.Deg2Rad) * distance;
            float y = Mathf.Sin(ran * Mathf.Deg2Rad) * distance;
            Vector3 pos = player.transform.position + new Vector3(x, y, 0);
            ObjectPooler.Instance.Get(enemy[Random.Range(0, enemy.Count)], pos, new Vector3(0, 0, 0));
            yield return new WaitForSeconds(spawnCoolDown);
        }
    }

    

}
