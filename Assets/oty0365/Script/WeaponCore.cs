using UnityEngine;

public class WeaponCore : MonoBehaviour
{
    [SerializeField] private float rotationNumber;
    private void Update()
    {
        gameObject.transform.position = PlayerStatus.Instance.gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.Euler(0,0,gameObject.transform.rotation.eulerAngles.z + rotationNumber*Time.deltaTime*PlayerStatus.Instance.PlayerAttackSpeed);
    }
}
