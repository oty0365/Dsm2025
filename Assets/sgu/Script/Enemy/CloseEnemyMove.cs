using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


public class CloseEnemyMove : MonoBehaviour, IEnemyMove, IPoolingObject
{
    private Transform player;
    private PlayerState playerState;
    
    
    private float firstDamageCooldown;
    
    [SerializeField]
    private float damageCooldown = 1f;
    [SerializeField]
    private int damage;

    private void Awake()
    {
        firstDamageCooldown = damageCooldown;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerState = player.GetComponent<PlayerState>();
    }

    [SerializeField]
    private float speed;

    private void Update()
    {
        damageCooldown -= Time.deltaTime;
    }

    public void Move()
    {
        StartCoroutine(EnemyMove());
        OnDeathInit();
        
    }
    
    public void Attack()
    {
        if (damageCooldown <= 0)
        {
            playerState.Health -= damage;
            damageCooldown = firstDamageCooldown;
        }
        
    }

    public void OnBirth()
    {
        Move();
    }

    public void OnDeathInit()
    {
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Attack();
        }
    }

    public IEnumerator EnemyMove()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            yield return null;
        }
    }
}
