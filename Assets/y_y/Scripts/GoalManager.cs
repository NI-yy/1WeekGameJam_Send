using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GoalManager : MonoBehaviour
{
    private int score_box = 0;
    [SerializeField] GameObject TimeManager;
    [SerializeField] GameObject ScoreManager;
    [SerializeField] GameObject ClearUIManager;
    [SerializeField] StageNumManager StageNumManager;
    [SerializeField] GameObject PoseUIManager;
    [SerializeField] ParticleSystem GoaledParticleSystem;
    [SerializeField] AudioClip itemInSE;
    [SerializeField] AudioClip playerInSE;

    private ScoreManager _scoreManager;
    private AudioSource _audioSource;
    float total_score = 0f;
    private bool is_over = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ResetScore");
        total_score = 0f;
        _scoreManager = ScoreManager.GetComponent<ScoreManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == "Player")
        {
            if (!(is_over))
            {
                _audioSource.PlayOneShot(playerInSE);

                Destroy(obj);
                PoseUIManager.GetComponent<PoseUIManager>().is_over = true;
                total_score = _scoreManager.CalScore();
                SaveManager.SaveAndSendScoreOnStage(StageNumManager.current_stage_num, (int)total_score);
                ClearUIManager.GetComponent<ClearUIManager_yy>().ActivateClearUI(total_score);

                is_over = true;
            }
            
        }
        else if (obj.tag == "PresentBox")
        {
            _audioSource.PlayOneShot(itemInSE);

            Destroy(obj);
            Instantiate(GoaledParticleSystem, transform.position, Quaternion.identity);
            _scoreManager.AddScore(10f);
        }
        else if (obj.tag == "BigPresentBox")
        {
            _audioSource.PlayOneShot(itemInSE);

            Destroy(obj);
            Instantiate(GoaledParticleSystem, transform.position, Quaternion.identity);
            _scoreManager.AddScore(200f);
        }
    }

    public void ResetScore()
    {

    }
}
