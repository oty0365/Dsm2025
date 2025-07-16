using UnityEngine;

public class HpUp : MonoBehaviour,IAugment,IPoolingObject
{
    [SerializeField] private float amount;
    public void Execute()
    {
        PlayerStatus.Instance.SetMaxHp(PlayerStatus.Instance.PlayerMaxHp+amount);
        PlayerStatus.Instance.SetHp(PlayerStatus.Instance.PlayerHp+amount);
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
