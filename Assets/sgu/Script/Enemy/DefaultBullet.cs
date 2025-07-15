using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DefaultBullet : MonoBehaviour, IBulletObject, IPoolingObject
{
    [SerializeField]
    private float bulletSpeed = 20f;
    [SerializeField]
    private int bulletDamage = 3;
    private Transform player;
    private Vector3 playerPosition;
    PlayerState playerState;
    private DamageEnemy damageEnemy;
    
    private void Awake()
    {
        damageEnemy = GetComponent<DamageEnemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerState = player.GetComponent<PlayerState>();
    }

    public void Move()
    {
        StartCoroutine(PlayerMove());
    }

    public IEnumerator PlayerMove()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * 10f);
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DamageEnemy.damageEnemy(bulletDamage);
            ObjectPooler.Instance.Return(gameObject);
        }
    }

    public void OnBirth()
    {
        playerPosition = player.position;
        Move();
    }

    public void OnDeathInit()
    {
    }
}
