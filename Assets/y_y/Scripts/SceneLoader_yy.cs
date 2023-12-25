using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader_yy : MonoBehaviour
{
    public static string nextSceneName;

    private void Awake()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
