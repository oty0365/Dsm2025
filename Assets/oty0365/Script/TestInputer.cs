using UnityEngine;

public class TestInputer : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerStatus.Instance.SetExp(PlayerStatus.Instance.PlayerExp+20);
        }
    }

}
