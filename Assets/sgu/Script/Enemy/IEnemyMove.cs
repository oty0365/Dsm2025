using System.Collections;
using UnityEngine;

public interface IEnemyMove
{
    public void Move();

    public void Attack();
    public IEnumerator EnemyMove();
}
