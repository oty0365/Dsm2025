using UnityEngine;

public class WeaponCore : HalfSingleMono<WeaponCore>
{
    public int maxUpgradeCount;
    [SerializeField] private float rotationNumber;
    private void Update()
    {
        gameObject.transform.position = PlayerStatus.Instance.gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.Euler(0,0,gameObject.transform.rotation.eulerAngles.z + rotationNumber*Time.deltaTime*PlayerStatus.Instance.PlayerAttackSpeed);
    }

    public void Equip(WeaponSetter setter)
    {
        bool weaponFound = false;
    
        for (var i = 0; i < gameObject.transform.childCount; i++)
        {
            WeaponSetter current = gameObject.transform.GetChild(i).gameObject.GetComponent<WeaponSetter>();
            if (current.weaponPrefab == setter.weaponPrefab)
            {
                weaponFound = true;
                if (current.currentUpgradePoint < maxUpgradeCount)
                {
                    current.currentUpgradePoint++;
                    current.SetWeapons();
                    if (current.currentUpgradePoint >= maxUpgradeCount)
                    {
                        AugmentManager.Instance.RemoveData(current.parentData);
                    }
                }
                break;
            }
        }

        if (!weaponFound)
        {
            var a = Instantiate(setter, gameObject.transform, false);
        }
    }
}
