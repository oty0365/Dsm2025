using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int level = 1;
    public float scoreTimer = 0f;
    
    public Text scoreText;
    public UIBlinker uiBlinker;
    public EnemyManager enemyManager;

    private void Update()
    {
        scoreTimer += Time.deltaTime;

        int newScore = Mathf.FloorToInt(scoreTimer);
        if (newScore > score)
        {
            score = newScore;
            UpdateScoreUI();

            if (score % 30 == 0)
            {
                LevelUp();
            }
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void LevelUp()
    {
        level++;

        uiBlinker.BlinkTwice();

        enemyManager.UpdateEnemyStats(level);
    }
}
