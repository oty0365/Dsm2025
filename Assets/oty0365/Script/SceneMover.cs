using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public int sceneIndex;
    void Start()
    {
        SceneManager.LoadScene(sceneIndex);
    }
    
}
