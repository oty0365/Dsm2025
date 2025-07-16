using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearAttackBoat : Enemy,IPoolingObject
{
    private void Start()
    {
        Initialize();
        var moveTowards = new NearAttackBoatMoveTowards(this);
        fsm.RegisterState("Run", moveTowards);
        fsm.ChangeState("Run");
    }
    public void OnBirth()
    {
        Initialize();
        var moveTowards = new NearAttackBoatMoveTowards(this);
        fsm.RegisterState("Run", moveTowards);
        fsm.ChangeState("Run");
    }

    public void OnDeathInit()
    {
        
    }
}
public class NearAttackBoatMoveTowards : IState
{
    private Enemy enemy;
        
    public NearAttackBoatMoveTowards(Enemy enemy)
    {
        this.enemy = enemy;
    }
        
    public void Enter()
    {
        enemy.rb2D.linearVelocity = (PlayerStatus.Instance.gameObject.transform.position-enemy.gameObject.transform.position).normalized*enemy.enemyData.moveSpeed;
    }

    public void FixedExecute()
    {
        enemy.rb2D.linearVelocity = (PlayerStatus.Instance.gameObject.transform.position-enemy.gameObject.transform.position).normalized*enemy.enemyData.moveSpeed;
    }
    public void Execute()
    { 
        enemy.FacePlayer();
    }
    public void Exit()
    {
        
    }
}
