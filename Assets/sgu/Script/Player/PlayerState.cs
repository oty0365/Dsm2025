using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    public int Health { get { return health; } set { health = value; } }

}
