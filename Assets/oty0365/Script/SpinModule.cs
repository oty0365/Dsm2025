using UnityEngine;

public class SpinModule : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0,0,gameObject.transform.rotation.eulerAngles.z + Time.deltaTime * spinSpeed);
    }
}
