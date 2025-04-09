using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void loadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
