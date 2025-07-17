using Unity.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float levelCycle = 30;
    private float firstLevelCycle;
    private ScoreManager scoreManager;
    private EnemySpawn enemySpawn;
    [ReadOnly]
    public int stageLevel = 1;
    void Start()
    {
        enemySpawn = GetComponent<EnemySpawn>();
        scoreManager = GetComponent<ScoreManager>();
        firstLevelCycle = levelCycle;
    }

    void Update()
    {
       /* if (Mathf.Approximately(levelCycle * stageLevel, scoreManager.Score))
        {
            stageLevel++;
            enemySpawn.spawnCoolDown *= (2f / 3f);
            enemySpawn.enemy.Add(enemySpawn.enemy[Random.Range(3, enemySpawn.enemy.Count)]);
            //Debug.Log($"스테이지 레벨: {stageLevel}");
            levelCycle = firstLevelCycle;
        }*/
    }
}
