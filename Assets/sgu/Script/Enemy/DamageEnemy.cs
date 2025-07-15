using System;
using System.Collections;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public float timeBetweenAttacks;
    private PlayerState playerState;
    bool isInvincible = false;
    public static Action<float> damageEnemy;
    
    private void Awake()
    {
        damageEnemy = (float attackDamage) => { PlayerAttackDamage(attackDamage); };
        playerState = GetComponent<PlayerState>();
    }

    public void PlayerAttackDamage(float damage)
    {
        if(!isInvincible && playerState.Health > 0)
        {
            playerState.Health -= damage;
            StartCoroutine(invincibility()); // 데미지 받은 후 무적 상태 시작
        }
    }


    IEnumerator invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(timeBetweenAttacks);
        isInvincible = false;
    }
}
