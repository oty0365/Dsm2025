using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private float health = 100;
    public float Health { get { return health; } set { health = value; } }

}
