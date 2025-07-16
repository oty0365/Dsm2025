using UnityEngine;

public class BoatRote : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 5f;

    void Update()
    {
        Vector2 inputDir = new Vector2(PlayerMove.Instance._horizontal, PlayerMove.Instance._vertical);
        if (inputDir.sqrMagnitude < 0.001f) return;
      
        float targetZAngle = Mathf.Atan2(inputDir.y, inputDir.x) * Mathf.Rad2Deg+270;

        float currentZAngle = transform.rotation.eulerAngles.z;
        float newZAngle = Mathf.LerpAngle(currentZAngle, targetZAngle, Time.deltaTime * rotateSpeed);
        transform.rotation = Quaternion.Euler(0f, 0f, newZAngle);
    }
}