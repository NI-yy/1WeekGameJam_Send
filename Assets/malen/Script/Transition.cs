using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public GameObject sceneTransitionImagesOut;

    void Start()
    {
        sceneTransitionImagesOut.SetActive(true);
        sceneTransitionImagesOut.GetComponent<SceneStart>().sceneTransitionFlag = true;
    }
}
