using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUi : HalfSingleMono<PlayerStatusUi>
{
    [SerializeField] private Slider playerExp;

    public void SetMaxExp(float exp)
    {
        playerExp.maxValue = exp;
    }
    public void SetExp(float exp)
    {
        playerExp.value = exp;
    }
    
}
