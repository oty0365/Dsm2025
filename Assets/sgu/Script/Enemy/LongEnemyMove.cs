using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class LongEnemyMove : MonoBehaviour, IPoolingObject, IEnemyMove
{
    private Transform player;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float stopTime = 1f;
    [SerializeField]
    private float attackCooldown = 1f;

    [SerializeField]
    private GameObject bullet;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    public void Move()
    {
        StartCoroutine(EnemyMove());
        OnDeathInit();
    }

    public void Attack()
    {
        ObjectPooler.Instance.Get(bullet, transform.position, new Vector3(0, 0, 0));
    }

    public IEnumerator EnemyMove()
    {
        float firstCooldown = attackCooldown;
        while (true)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (distanceToPlayer > distance || attackCooldown >= 0)
            {
                attackCooldown -= Time.deltaTime;
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(stopTime);
                Attack();
                attackCooldown = firstCooldown;
            }
        }
    }

    public void OnBirth()
    {
        Move();
    }

    public void OnDeathInit()
    {
    }

}
