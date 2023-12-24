using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using static Unity.VisualScripting.Member;
using UnityEngine.Rendering;
using System.Diagnostics.Contracts;

public class SelectLevel : MonoBehaviour
{

    public AudioClip transitionClip;
    public int level;
    public float animationInterval;
    public float expansionRate;
    public Vector3 moveTargetPoint;
    public float animationDuration;
    public CharacterFacing facing;
    public Vector3 characterTargetCoordinate;
    public GameObject character;
    public Sprite normalSprite;
    public Sprite kickSprite;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI highScoreText;
    public GameObject sceneTransitionImages;
    public Button[] allButtons;

    private Tween hoverTween;
    private Tween levelNumberTween;
    private Tween highScoreTween;

    public enum CharacterFacing
    {
        left,
        right
    }

    private void Start()
    {
        if (level - 1 <= SaveManager.GetStageFinishNumber())
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void CharacterMove()
    {
        Transform characterTransform = character.transform;
        characterTransform.position = characterTargetCoordinate;
        characterTransform.DOMove(characterTransform.position - new Vector3(0, 0.3f, 0), 0.15f);
        Vector3 scale = characterTransform.localScale;
        characterTransform.localScale = new Vector3(facing == CharacterFacing.right ? Mathf.Abs(scale.x) : Mathf.Abs(scale.x) * -1, scale.y, scale.z);
    }

    public void StartAnimation()
    {
        if (hoverTween != null)
            hoverTween.Kill();
        hoverTween = transform.DOScale(expansionRate, animationInterval).SetLoops(-1, LoopType.Yoyo);
        levelText.text = "" + level;
        levelNumberTween = levelText.transform.DOScale(1.5f, 0.1f).SetLoops(2, LoopType.Yoyo);
        highScoreText.text = $"{SaveManager.GetScoreOnStage(level)}";
        highScoreTween = highScoreText.transform.DOScale(1.5f, 0.1f).SetLoops(2, LoopType.Yoyo);
    }

    public void StopAnimation()
    {
        if (hoverTween != null)
            hoverTween.Kill();
        transform.DOScale(1f, animationInterval);
        levelNumberTween.Kill();
        levelText.transform.localScale = Vector3.one;
        highScoreTween.Kill();
        highScoreText.transform.localScale = Vector3.one;
    }

    public void StartLevel()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.PlayOneShot(transitionClip);

        Animator animator = character.GetComponent<Animator>();
        animator.SetTrigger("kick_trigger");

        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].interactable = false;
        }

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + moveTargetPoint, animationDuration).SetEase(Ease.Linear));
        sequence.Join(transform.DORotate(new Vector3(0, 0, 1800), animationDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.OnComplete(() => sceneTransitionImages.GetComponent<SceneTransition>().SceneTransitionStart(level));
    }
}
