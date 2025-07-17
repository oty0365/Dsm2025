using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    private EnemyData _instanceData; 
    public TrailRenderer trailRenderer;
    public Damagable hitModule;
    public Rigidbody2D rb2D;
    public Fsm fsm;
    public SpriteRenderer sr;
    protected MaterialPropertyBlock _metProps;
    protected Coroutine _currentHitFlow;
    public GameObject hitEffect;

    private float _currentHp;
    public float CurrentHp
    {
        get => _currentHp;
        set
        {
            if (_currentHp != value)
                _currentHp = value;

            if (_currentHp <= 0)
            {
                Drop();
                Death();
            }
        }
    }

    public void Initialize()
    {
        _metProps = new MaterialPropertyBlock();
        sr.GetPropertyBlock(_metProps);
        _metProps.SetFloat("_Progress", 0);
        sr.SetPropertyBlock(_metProps);
        _instanceData = Instantiate(enemyData);
        trailRenderer.Clear();
        ApplyStageBuffs();
        CurrentHp = _instanceData.health;
    }

    private void ApplyStageBuffs()
    {
        int currentStage = ScoreManager.Instance.Score; 

        int buffCount = currentStage / 15;

        gameObject.GetComponent<Damagable>().damage += (int)(buffCount*0.7);
        _instanceData.health += buffCount * 3;
        _instanceData.expAmount += buffCount * 3;
    }

    public void FacePlayer()
    {
        var dir = PlayerStatus.Instance.transform.position - transform.position;
        float distance = dir.magnitude;
        float minDistance = 0.1f;
        if (distance > minDistance)
        {
            float deg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, deg);
        }
    }

    private IEnumerator HitFlow()
    {
        for (float i = 0f; i < 1.2f; i += Time.deltaTime * 10f)
        {
            sr.GetPropertyBlock(_metProps);
            _metProps.SetFloat("_Progress", i);
            sr.SetPropertyBlock(_metProps);
            yield return null;
        }

        sr.GetPropertyBlock(_metProps);
        _metProps.SetFloat("_Progress", 1);
        sr.SetPropertyBlock(_metProps);

        for (float i = 1.2f; i > 0f; i -= Time.deltaTime * 10f)
        {
            sr.GetPropertyBlock(_metProps);
            _metProps.SetFloat("_Progress", i);
            sr.SetPropertyBlock(_metProps);
            yield return null;
        }

        sr.GetPropertyBlock(_metProps);
        _metProps.SetFloat("_Progress", 0);
        sr.SetPropertyBlock(_metProps);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            Hit(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            if (!PlayerStatus.Instance.isInfinite)
            {
                PlayerStatus.Instance.GetDamage(hitModule.damage, hitModule.infiniteTime);
            }
        }
    }

    public virtual void Drop()
    {
        var a = ObjectPooler.Instance.Get(ExpBank.Instance.exp, transform.position, new Vector3(0, 0, 45));
        a.GetComponent<ExpGiver>().expAmount = _instanceData.expAmount;
    }

    public virtual void Hit(GameObject caster)
    {
        float totalDamage = caster.GetComponent<WeaponModule>().damage + PlayerStatus.Instance.PlayerAtk;
        CurrentHp -= totalDamage;

        ObjectPooler.Instance.Get(hitEffect, transform.position, new Vector3(-90, 0, 0));
        SoundManager.Instance.PlaySFX("Hit");
        if (_currentHitFlow != null)
        {
            StopCoroutine(_currentHitFlow);
        }

        if (gameObject.activeSelf)
        {
            _currentHitFlow = StartCoroutine(HitFlow());
        }
    }

    public virtual void Death()
    {
        ObjectPooler.Instance.Return(gameObject);
    }
}
