using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using static Unity.VisualScripting.Member;

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

    private Tween hoverTween;

    public enum CharacterFacing
    {
        left,
        right
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
    }

    public void StopAnimation()
    {
        if (hoverTween != null) 
            hoverTween.Kill();
        transform.DOScale(1f, animationInterval);
    }

    public void StartLevel()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.PlayOneShot(transitionClip);

        SpriteRenderer spriteRenderer = character.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = kickSprite;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + moveTargetPoint, animationDuration).SetEase(Ease.Linear));
        sequence.Join(transform.DORotate(new Vector3(0, 0, 1800), animationDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear));
        sequence.OnComplete(() => SceneManager.LoadScene("Stage" + level.ToString()));
    }
}
