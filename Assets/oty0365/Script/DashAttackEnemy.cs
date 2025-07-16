using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttackEnemy : Enemy, IPoolingObject
{
    public float dashDistance = 15f;
    public float dashDelay = 2f;
    public float dashSpeed = 20f;
    public float stopDistance = 0.1f;
    public GameObject dashTargetIndicator;
    public LineRenderer lineRenderer;
    
    private Vector2 dashTarget;
    
    private void Start()
    {
        Initialize();
        var moveTowards = new DashAttackEnemyMoveTowards(this);
        var prepare = new DashAttackEnemyPrepare(this);
        var dash = new DashAttackEnemyDash(this);
        fsm.RegisterState("Move", moveTowards);
        fsm.RegisterState("Prepare", prepare);
        fsm.RegisterState("Dash", dash);
        fsm.ChangeState("Move");
    }
    
    public void OnBirth()
    {
        Initialize();
        var moveTowards = new DashAttackEnemyMoveTowards(this);
        var prepare = new DashAttackEnemyPrepare(this);
        var dash = new DashAttackEnemyDash(this);
        fsm.RegisterState("Move", moveTowards);
        fsm.RegisterState("Prepare", prepare);
        fsm.RegisterState("Dash", dash);
        fsm.ChangeState("Move");
    }

    public void OnDeathInit()
    {
        
    }
    
    public float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, PlayerStatus.Instance.gameObject.transform.position);
    }
    
    public void CalculateDashTarget()
    {
        Vector2 playerPos = PlayerStatus.Instance.gameObject.transform.position;
        Vector2 direction = (playerPos - (Vector2)transform.position).normalized;
        dashTarget = (Vector2)transform.position + direction * dashDistance;
    }
    
    public Vector2 GetDashTarget()
    {
        return dashTarget;
    }
    
    public void ShowDashIndicator()
    {
        if (dashTargetIndicator != null)
        {
            dashTargetIndicator.SetActive(true);
            dashTargetIndicator.transform.position = dashTarget;
        }
        
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, dashTarget);
        }
    }
    
    public void HideDashIndicator()
    {
        if (dashTargetIndicator != null)
        {
            dashTargetIndicator.SetActive(false);
        }
        
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }
}

public class DashAttackEnemyMoveTowards : IState
{
    private DashAttackEnemy enemy;
        
    public DashAttackEnemyMoveTowards(DashAttackEnemy enemy)
    {
        this.enemy = enemy;
    }
        
    public void Enter()
    {
        enemy.rb2D.linearVelocity = (PlayerStatus.Instance.gameObject.transform.position - enemy.gameObject.transform.position).normalized * enemy.enemyData.moveSpeed;
    }

    public void FixedExecute()
    {
        enemy.rb2D.linearVelocity = (PlayerStatus.Instance.gameObject.transform.position - enemy.gameObject.transform.position).normalized * enemy.enemyData.moveSpeed;
    }
    
    public void Execute()
    {
        enemy.FacePlayer();
        
        if (enemy.GetDistanceToPlayer() <= enemy.enemyData.attackRange)
        {
            enemy.fsm.ChangeState("Prepare");
        }
    }
    
    public void Exit()
    {
        enemy.rb2D.linearVelocity = Vector2.zero;
    }
}

public class DashAttackEnemyPrepare : IState
{
    private DashAttackEnemy enemy;
    private float prepareTimer;
    
    public DashAttackEnemyPrepare(DashAttackEnemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        prepareTimer = 0f;
        enemy.CalculateDashTarget();
        enemy.ShowDashIndicator();
    }

    public void FixedExecute()
    {
        
    }
    
    public void Execute()
    {
        enemy.FacePlayer();
        
        if (enemy.GetDistanceToPlayer() > enemy.enemyData.attackRange)
        {
            enemy.fsm.ChangeState("Move");
            return;
        }
        
        prepareTimer += Time.deltaTime;
        
        if (prepareTimer >= enemy.dashDelay)
        {
            enemy.fsm.ChangeState("Dash");
        }
    }
    
    public void Exit()
    {
        enemy.HideDashIndicator();
    }
}

public class DashAttackEnemyDash : IState
{
    private DashAttackEnemy enemy;
    
    public DashAttackEnemyDash(DashAttackEnemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        Vector2 direction = (enemy.GetDashTarget() - (Vector2)enemy.transform.position).normalized;
        enemy.rb2D.linearVelocity = direction * enemy.dashSpeed;
    }

    public void FixedExecute()
    {
        
    }
    
    public void Execute()
    {
        float distanceToTarget = Vector2.Distance(enemy.transform.position, enemy.GetDashTarget());
        
        if (distanceToTarget <= enemy.stopDistance)
        {
            enemy.fsm.ChangeState("Move");
        }
    }
    
    public void Exit()
    {
        enemy.rb2D.linearVelocity = Vector2.zero;
    }
}