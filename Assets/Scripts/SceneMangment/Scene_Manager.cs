using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    #region Singleton
    public static Scene_Manager Instance;
    #endregion
    void Awake()
    {
        Instance = this;
    }

    public void GoToSceneByName(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
