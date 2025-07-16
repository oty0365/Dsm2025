using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackBoat : Enemy, IPoolingObject
{
    public float attackDelay = 2f;
    public GameObject bullet;
    
    private void Start()
    {
        Initialize();
        var moveTowards = new RangedAttackBoatMoveTowards(this);
        var attack = new RangedAttackBoatAttack(this);
        fsm.RegisterState("Move", moveTowards);
        fsm.RegisterState("Attack", attack);
        fsm.ChangeState("Move");
    }
    
    public void OnBirth()
    {
        Initialize();
        var moveTowards = new RangedAttackBoatMoveTowards(this);
        var attack = new RangedAttackBoatAttack(this);
        fsm.RegisterState("Move", moveTowards);
        fsm.RegisterState("Attack", attack);
        fsm.ChangeState("Move");
    }

    public void OnDeathInit()
    {
        
    }
    
    public float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, PlayerStatus.Instance.gameObject.transform.position);
    }
}

public class RangedAttackBoatMoveTowards : IState
{
    private RangedAttackBoat boat;
        
    public RangedAttackBoatMoveTowards(RangedAttackBoat boat)
    {
        this.boat = boat;
    }
        
    public void Enter()
    {
        boat.rb2D.linearVelocity = (PlayerStatus.Instance.gameObject.transform.position - boat.gameObject.transform.position).normalized * boat.enemyData.moveSpeed;
    }

    public void FixedExecute()
    {
        boat.rb2D.linearVelocity = (PlayerStatus.Instance.gameObject.transform.position - boat.gameObject.transform.position).normalized * boat.enemyData.moveSpeed;
    }
    
    public void Execute()
    {
        boat.FacePlayer();
        
        if (boat.GetDistanceToPlayer() <= boat.enemyData.attackRange)
        {
            boat.fsm.ChangeState("Attack");
        }
    }
    
    public void Exit()
    {
        boat.rb2D.linearVelocity = Vector2.zero;
    }
}

public class RangedAttackBoatAttack : IState
{
    private RangedAttackBoat boat;
    private float attackTimer;
    
    public RangedAttackBoatAttack(RangedAttackBoat boat)
    {
        this.boat = boat;
    }
    
    public void Enter()
    {
        attackTimer = 0f;
    }

    public void FixedExecute()
    {
        
    }
    
    public void Execute()
    {
        boat.FacePlayer();
        
        if (boat.GetDistanceToPlayer() > boat.enemyData.attackRange)
        {
            boat.fsm.ChangeState("Move");
            return;
        }
        
        attackTimer += Time.deltaTime;
        
        if (attackTimer >= boat.attackDelay)
        {
            FireBullet();
            attackTimer = 0f;
        }
    }
    
    public void Exit()
    {
        
    }
    
    private void FireBullet()
    {
        Debug.Log("Fire Bullet");
        var a= ObjectPooler.Instance.Get(boat.bullet, boat.gameObject.transform.position, new Vector3(0,0,0));
        Vector2 direction = (PlayerStatus.Instance.gameObject.transform.position - boat.transform.position);
        a.GetComponent<FlyingBoom>().Fire(direction);
    }
}