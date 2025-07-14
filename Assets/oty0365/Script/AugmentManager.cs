using System;
using System.Collections.Generic;
using UnityEngine;

public class AugmentManager : HalfSingleMono<AugmentManager>
{
    public event Action<AugmentData[]> setUi;
    //private Dictionary<AugmentType, Action> augments = new();
    [SerializeField] AugmentDatas augmentDatas;
    /*public void UpLoad(AugmentType type,Action action)
    {
        if (augments.ContainsKey(type))
        {
            augments[type] =  action;
        }
        else
        {
            augments.Add(type,action);
        }
    }

    public void Execute(AugmentType type)
    {
        if (augments.ContainsKey(type))
        {
            augments[type]?.Invoke();
        }
    }*/

    public void AugmentSelection()
    {
        setUi?.Invoke(GetRandomAugments());
    }
    
    private AugmentData[] GetRandomAugments()
    {
        if (augmentDatas.datas.Length < 3)
        {
            Debug.LogWarning("어그먼트 데이터가 3개 미만입니다.");
            return augmentDatas.datas;
        }

        List<AugmentData> dataList = new List<AugmentData>(augmentDatas.datas);
        
        for (var i = 0; i < 3; i++)
        {
            var randIndex = UnityEngine.Random.Range(i, dataList.Count);
            (dataList[i], dataList[randIndex]) = (dataList[randIndex], dataList[i]);
        }

        return new AugmentData[] { dataList[0], dataList[1], dataList[2] };
    }
    
}
