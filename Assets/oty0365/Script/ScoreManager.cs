using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class ScoreManager : HalfSingleMono<ScoreManager>
{
    public float scoreCoolTime = 1f;
    //private float firstCoolTime;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int _score;
    
    public int Score
    {
        get=>_score;
        private set
        { 
            _score = value;
            scoreText.text = _score.ToString();
            EnemySpawn.Instance.TryUnlockEnemies(_score); 
        }
    }
    void Start()
    {
        StartCoroutine(TimerFlow());
    }

    /*void Update()
    {
        scoreCoolTime -= Time.deltaTime;
        if (scoreCoolTime <= 0)
        {
            scoreCoolTime = firstCoolTime;
            Score++;
        }
    }*/
    private IEnumerator TimerFlow()
    {
        while (true)
        {
            yield return new WaitForSeconds(scoreCoolTime);
            Score++;
        }
    }
}
