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


    [HideInInspector] public int transitionLevel;
    [HideInInspector] public string transitionSceneName;
    public bool sceneTransitionFlag = false;
    private float sceneTransitionTime = 0;
    private int lastStartedTransitionObjectIndex = -1;

    private bool toStage;

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
                if (toStage)
                {
                    SceneManager.LoadScene("Stage" + transitionLevel.ToString());
                }
                else
                {
                    SceneManager.LoadScene(transitionSceneName);
                }
                
            }
        }
    }

    public void SceneTransitionStart(int level)
    {
        transitionLevel = level;
        sceneTransitionFlag = true;
        toStage = true;
    }

    public void SceneTransStartNoStage(string name)
    {
        transitionSceneName = name;
        sceneTransitionFlag = true;
        toStage = false;
    }
}
