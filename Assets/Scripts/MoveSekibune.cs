using UnityEngine;

public class MoveSekibune : MonoBehaviour
{
    public float speed;
    public float stopDistance;
    private float direction;
    
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    private void Update()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            
            if (target.position.x > transform.position.x)
            {
                Flip(false);
            }
            else
            {
                Flip(true);
            }
        }
    }
    
    public void Flip(bool facingRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
