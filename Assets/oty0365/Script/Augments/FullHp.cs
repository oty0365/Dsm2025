using UnityEngine;

public class FullHp : MonoBehaviour,IAugment,IPoolingObject
{
    //[SerializeField] private float amount;
    public void Execute()
    {
        //플레이어 hp++
        OnDeathInit();
        ObjectPooler.Instance.Return(gameObject);
    }

    public void OnBirth()
    {
        Execute();
    }

    public void OnDeathInit()
    {

    }
}
