using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _sharedInstance = null;
    
    public static SceneManager Instance { get { return _sharedInstance; } }

    private void Awake()
    {
        if (_sharedInstance == null)
        {
            _sharedInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
