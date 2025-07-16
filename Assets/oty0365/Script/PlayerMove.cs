using UnityEngine;

public class PlayerMove : HalfSingleMono<PlayerMove>
{
    [SerializeField] private Rigidbody2D rb2D;
    public float _horizontal;
    public float _vertical;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(_horizontal, _vertical).normalized*PlayerStatus.Instance.PlayerMoveSpeed;
    }
    
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }
}
