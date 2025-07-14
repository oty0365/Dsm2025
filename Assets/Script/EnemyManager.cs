using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float baseHealth = 100f;
    public float baseSpeed = 2f;
    public float baseDamage = 10f;

    public void UpdateEnemyStats(int level)
    {
        float health = baseHealth + (level - 1) * 50f;
        float speed = baseSpeed + (level - 1) * 0.5f;
        float damage = baseDamage + (level - 1) * 5f;
    }
}
