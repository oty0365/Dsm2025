using UnityEngine;
using System.Collections;

public class CammeraManager : HalfSingleMono<CammeraManager>
{
    [SerializeField] private float followSpeed = 10f;

    private Transform _currentTarget;
    private Coroutine _followRoutine;

    void Start()
    {
        SetTarget(PlayerStatus.Instance.gameObject.transform);
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null) return;

        _currentTarget = newTarget;
        
        if (_followRoutine != null)
            StopCoroutine(_followRoutine);

        _followRoutine = StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        while (true)
        {
            if (_currentTarget != null)
            {
                Vector3 targetPos = _currentTarget.position;
                targetPos.z = transform.position.z; 
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * followSpeed);
            }

            yield return new WaitForFixedUpdate(); 
        }
    }
}