using UnityEngine;

public class Augments : MonoBehaviour
{
    void Start()
    {
        AugmentManager.Instance.UpLoad(AugmentType.HpUp,HpUp);
        AugmentManager.Instance.UpLoad(AugmentType.MoveSpeedUp,MoveSpeedUp);
        AugmentManager.Instance.UpLoad(AugmentType.Boom,Boom);
        AugmentManager.Instance.UpLoad(AugmentType.Execute,Execute);
        AugmentManager.Instance.UpLoad(AugmentType.Shiled,Shiled);
        AugmentManager.Instance.UpLoad(AugmentType.Knife,Knife);
        AugmentManager.Instance.UpLoad(AugmentType.MoreBullet,MoreBullet);
        AugmentManager.Instance.UpLoad(AugmentType.FullHp,FullHp);
        
    }

    private void HpUp()
    {
        //플레이어 최대체력 증가 로직
    }

    private void MoveSpeedUp()
    {
        //
    }

    private void MoreBullet()
    {
        //
    }

    private void Shiled()
    {
        //
    }

    private void Execute()
    {
        //
    }

    private void Knife()
    {
        //
    }

    private void Boom()
    {
        //
    }

    private void FullHp()
    {
        //
    }
    
}
