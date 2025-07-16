using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
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
        get=>_currentHp;
        set{
            if(_currentHp!=value){
                _currentHp=value;
            }

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
        CurrentHp = enemyData.health;
    }
    public void FacePlayer()
    {
        var dir = PlayerStatus.Instance.gameObject.transform.position - gameObject.transform.position;
        float distance = dir.magnitude;
        float minDistance = 0.1f;
        if (distance > minDistance)
        {
            var deg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, deg);
        }
    }
    private IEnumerator HitFlow()
    {
        for (var i = 0f; i < 1.2f; i += Time.deltaTime * 10f)
        {
            sr.GetPropertyBlock(_metProps);
            _metProps.SetFloat("_Progress", i);
            sr.SetPropertyBlock(_metProps);
            yield return null;
        }
        sr.GetPropertyBlock(_metProps);
        _metProps.SetFloat("_Progress", 1);
        sr.SetPropertyBlock(_metProps);

        for (var i = 1.2f; i > 0f; i -= Time.deltaTime * 10f)
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
            //Debug.Log(other.name);
            if (!PlayerStatus.Instance.isInfinite)
            {
                PlayerStatus.Instance.GetDamage(hitModule.damage,hitModule.infiniteTime);
            }
        }
    }

    public virtual void Drop()
    {
        var a = ObjectPooler.Instance.Get(ExpBank.Instance.exp,gameObject.transform.position,new Vector3(0,0,45f));
        a.GetComponent<ExpGiver>().expAmount = enemyData.expAmount;
    }
    public virtual void Hit(GameObject caster)
    {
        CurrentHp-=caster.GetComponent<WeaponModule>().damage+PlayerStatus.Instance.PlayerAtk;
        ObjectPooler.Instance.Get(hitEffect,gameObject.transform.position,new Vector3(-90,0,0));
        
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