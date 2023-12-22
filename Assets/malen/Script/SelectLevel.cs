using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class SelectLevel : MonoBehaviour
{

    public AudioClip transitionClip;
    public int level;
    public float animationInterval;
    public float expansionRate;

    private bool isHoverd = false;
    private Sequence expansionSequence;

    public void ChangeScene()
    {
        SceneManager.LoadScene("Stage" + level.ToString());
        AudioSource source = GetComponent<AudioSource>();
        source.PlayOneShot(transitionClip);
    }

    private void Start()
    {
        DOTween.Init();
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = level.ToString();
        expansionSequence = DOTween.Sequence();
    }

    public void StartAnimation()
    {
        var expansionSequence = DOTween.Sequence();
        expansionSequence.Append(transform.DOScale(expansionRate, animationInterval));
        expansionSequence.Append(transform.DOScale(1f, animationInterval));
        expansionSequence.SetLoops(-1);
    }

    public void StopAnimation()
    {
        expansionSequence.Kill();
    }
}
