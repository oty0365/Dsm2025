using UnityEngine;

public class Oar : MonoBehaviour,IAugment,IPoolingObject
{
    [SerializeField] private WeaponSetter setter;
    public void Execute()
    {
        WeaponCore.Instance.Equip(setter);
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
