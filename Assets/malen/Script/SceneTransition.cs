using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public SceneTransitionObject[] sceneTransitionObjects;
    public float sceneTransitionStartInterval;
    public float sceneTransitionSpeed;
    public float timeUpToSceneTransition;

    public bool sceneTransitionFlag = false;
    private int transitionLevel;
    private float sceneTransitionTime = 0;
    private int lastStartedTransitionObjectIndex = -1;
    private string transitionSceneName;

    [Serializable]
    public class SceneTransitionObject
    {
        public GameObject transitionObject;
        public Vector3 targetPoint;
    }

    public void FixedUpdate()
    {
        if (sceneTransitionFlag)
        {
            sceneTransitionTime += Time.deltaTime;
            if (lastStartedTransitionObjectIndex < sceneTransitionObjects.Length - 1) {
                if (sceneTransitionTime > (lastStartedTransitionObjectIndex + 1) * sceneTransitionStartInterval)
                {
                    sceneTransitionObjects[lastStartedTransitionObjectIndex + 1].transitionObject.transform.DOLocalMove(sceneTransitionObjects[lastStartedTransitionObjectIndex + 1].targetPoint, sceneTransitionSpeed);
                    lastStartedTransitionObjectIndex++;
                }
            }
            if(timeUpToSceneTransition < sceneTransitionTime)
            {
                SceneManager.LoadScene(transitionSceneName);
            }
        }
    }

    public void SceneTransitionStart(string sceneName)
    {
        transitionSceneName = sceneName;
        sceneTransitionFlag = true;
    }
}
