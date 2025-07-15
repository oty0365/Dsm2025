using UnityEngine;

public class WeaponSetter : MonoBehaviour
{
    [Header("Weapon Settings")]
    public GameObject weaponPrefab; 
    public float distance = 2f;     
    
    void Start()
    {
        CreateWeapons();
    }
    
    void CreateWeapons()
    {
        if (weaponPrefab == null)
        {
            Debug.LogError("Weapon prefab is not assigned!");
            return;
        }
        
        float angleStep = 360f / PlayerStatus.Instance.PlayerBulletCount;  
        
        for (int i = 0; i < PlayerStatus.Instance.PlayerBulletCount; i++)
        {
            float angle = i * angleStep;  
            float radian = angle * Mathf.Deg2Rad;  
            
            float x = Mathf.Cos(radian) * distance;
            float y = Mathf.Sin(radian) * distance;
            
            Vector3 weaponPosition = transform.position + new Vector3(x, y, 0);

            GameObject weapon = Instantiate(weaponPrefab, weaponPosition, Quaternion.identity);
            
            weapon.transform.SetParent(transform);
            
            weapon.transform.rotation = Quaternion.Euler(0, 0, angle-90);
            
            weapon.name = $"Weapon_{i}_{angle}deg";
        }
    }
}