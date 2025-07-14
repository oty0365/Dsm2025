using UnityEngine;

public class Boom : MonoBehaviour,IAugment,IPoolingObject
{
    //[SerializeField] private float amount;
    public void Execute()
    {
        //폭탄 배치
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
