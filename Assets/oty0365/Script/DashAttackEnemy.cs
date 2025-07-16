using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttackEnemy : Enemy, IPoolingObject
{
    public float dashDistance = 15f;
    public float dashDelay = 2f;
    public float dashSpeed = 20f;
    public float stopDistance = 0.1f;
    public float lineExtendSpeed = 2f;
    public float dashCooldown = 3f;
    public GameObject dashTargetIndicator;
    public LineRenderer lineRenderer;
    
    private Vector2 dashTarget;
    private float lastDashTime;
    
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
        lastDashTime = -dashCooldown; // 처음 생성 시 바로 대쉬 가능하도록
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
        HideDashIndicator();
    }
    
    public bool CanDash()
    {
        return Time.time >= lastDashTime + dashCooldown;
    }
    
    public void StartDash()
    {
        lastDashTime = Time.time;
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
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
        
        if (dashTargetIndicator != null)
        {
            dashTargetIndicator.SetActive(true);
            dashTargetIndicator.transform.SetParent(null);
            dashTargetIndicator.transform.position = transform.position;
        }
    }
    
    public void UpdateIndicatorPosition(Vector2 position)
    {
        if (lineRenderer != null && lineRenderer.enabled)
        {
            lineRenderer.SetPosition(1, position);
        }
        
        if (dashTargetIndicator != null && dashTargetIndicator.activeInHierarchy)
        {
            dashTargetIndicator.transform.position = position;
        }
    }
    
    // 라인렌더러의 시작점을 자신의 위치로 업데이트하는 새로운 메소드
    public void UpdateLineStartToSelf()
    {
        if (lineRenderer != null && lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, transform.position);
        }
    }
    
    public void HideDashIndicator()
    {
        if (dashTargetIndicator != null)
        {
            dashTargetIndicator.SetActive(false);
            dashTargetIndicator.transform.SetParent(transform);
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
        
        if (enemy.GetDistanceToPlayer() <= enemy.enemyData.attackRange && enemy.CanDash())
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
    private Vector2 indicatorStartPos;
    private Vector2 indicatorTargetPos;
    
    public DashAttackEnemyPrepare(DashAttackEnemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        prepareTimer = 0f;
        enemy.CalculateDashTarget();
        indicatorStartPos = enemy.transform.position;
        indicatorTargetPos = enemy.GetDashTarget();
        enemy.ShowDashIndicator();
        enemy.FacePlayer();
    }

    public void FixedExecute()
    {
        
    }
    
    public void Execute()
    {
        
        prepareTimer += Time.deltaTime;
        
        float progress = (prepareTimer * enemy.lineExtendSpeed) / enemy.dashDelay;
        progress = Mathf.Clamp01(progress);
        
        Vector2 currentIndicatorPos = Vector2.Lerp(indicatorStartPos, indicatorTargetPos, progress);
        enemy.UpdateIndicatorPosition(currentIndicatorPos);
        
        if (prepareTimer >= enemy.dashDelay)
        {
            enemy.fsm.ChangeState("Dash");
        }
    }
    
    public void Exit()
    {
        
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
        Vector2 worldDashTarget = enemy.GetDashTarget();
        Vector2 direction = (worldDashTarget - (Vector2)enemy.transform.position).normalized;
        enemy.rb2D.linearVelocity = direction * enemy.dashSpeed;
        enemy.StartDash(); // 대쉬 시작 시간 기록
    }

    public void FixedExecute()
    {
        
    }
    
    public void Execute()
    {
        // 대쉬 중에 라인렌더러의 시작점을 자신의 위치로 계속 업데이트
        enemy.UpdateLineStartToSelf();
        
        float distanceToTarget = Vector2.Distance(enemy.transform.position, enemy.GetDashTarget());
        
        if (distanceToTarget <= enemy.stopDistance)
        {
            enemy.HideDashIndicator();
            enemy.fsm.ChangeState("Move");
        }
    }
    
    public void Exit()
    {
        enemy.rb2D.linearVelocity = Vector2.zero;
    }
}