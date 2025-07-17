using UnityEngine;

public class SpeedUp : MonoBehaviour,IAugment,IPoolingObject
{
    [SerializeField] private float amount;
    public void Execute()
    {
        PlayerStatus.Instance.SetAtkSpeed(PlayerStatus.Instance.PlayerAttackSpeed+amount);
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