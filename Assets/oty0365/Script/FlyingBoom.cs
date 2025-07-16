using System;
using System.Collections;
using UnityEngine;

public class FlyingBoom : MonoBehaviour,IPoolingObject
{
    public float speed;
    public string hitTagName;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float lifeSpan;
    [SerializeField] private TrailRenderer trailRenderer;
    public void OnBirth()
    {
        trailRenderer.Clear();
       StartCoroutine(LifeSpan());
       
    }

    public void Fire(Vector2 dir)
    {
        rb2D.linearVelocity = dir.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(hitTagName))
        {
            if (hitTagName == "Player")
            {
                var a = gameObject.GetComponent<Damagable>();
                PlayerStatus.Instance.GetDamage(a.damage,a.infiniteTime);
            }
            ObjectPooler.Instance.Return(gameObject);
        }
    }

    IEnumerator LifeSpan()
    {
     
        yield return new WaitForSeconds(lifeSpan);
        ObjectPooler.Instance.Return(gameObject);   
    }

    public void OnDeathInit()
    {
    }
}
