using System;
using System.Collections;
using UnityEngine;

public class BoomHolder : MonoBehaviour
{
    public GameObject boom;
    public static float duration = 2f;

    private void Start()
    {
        StartCoroutine(BoomingFlow());
    }

    private IEnumerator BoomingFlow()
    {
        while (true)
        {
            var a=ObjectPooler.Instance.Get(boom,gameObject.transform.position,new Vector3(0,0,0));
            var dir = -1*(PlayerStatus.Instance.gameObject.transform.position-gameObject.transform.position);
            a.GetComponent<FlyingBoom>().Fire(dir);
            yield return new WaitForSeconds(duration);
        }

    }
}
