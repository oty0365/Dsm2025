using UnityEngine;

public class SFXObj : MonoBehaviour,IPoolingObject
{
    public AudioSource currentSource;
    void Start()
    {
        
    }

    void Update()
    {
        if (!currentSource.isPlaying)
        {
            ObjectPooler.Instance.Return(gameObject);
        }
    }
    public void OnBirth()
    {
        
    }
    public void OnDeathInit()
    {
        
    }
}
