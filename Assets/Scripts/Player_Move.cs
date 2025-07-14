using UnityEngine;

public class Player_Move : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 scale;

    private float moveX;
    private float moveY;
    public float moveSpeed = 3;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector3(moveX * moveSpeed, moveY * moveSpeed,0);
        
        if (moveX < 0)
        {
            scale.y = -1;
        }else if (moveX > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;
        
        
    }
}
