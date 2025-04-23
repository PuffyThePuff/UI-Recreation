using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _sharedInstance = null;
    
    public static SceneManager Instance { get { return _sharedInstance; } }

    void Awake()
    {
        _sharedInstance = this;
    }

    public void LoadSceneAsync(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }
}
