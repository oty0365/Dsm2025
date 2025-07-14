using UnityEngine;

public class Knife : MonoBehaviour,IAugment,IPoolingObject
{
    //[SerializeField] private float amount;
    public void Execute()
    {
        //칼 배치
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
