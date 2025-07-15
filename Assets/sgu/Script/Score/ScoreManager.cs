using Unity.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float scoreCoolTime = 1f;
    private float firstCoolTime;
    
    [ReadOnly]
    public int score = 0;
    void Start()
    {
        firstCoolTime = scoreCoolTime;
    }

    void Update()
    {
        scoreCoolTime -= Time.deltaTime;
        if (scoreCoolTime <= 0)
        {
            scoreCoolTime = firstCoolTime;
            score++;
        }
    }
}
