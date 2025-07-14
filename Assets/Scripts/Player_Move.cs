using UnityEngine;

public class Player_Move : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed;
    private float moveX;
    private float moveY;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector3(moveX * moveSpeed, moveY * moveSpeed, 0);
    }
}
