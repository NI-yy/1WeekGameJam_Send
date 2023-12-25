using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class PlayerActionToPresentBox : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private Transform rayPointForLeft;

    [SerializeField] float rayRadius = 1.0f;
    [SerializeField] float rayDistance = 0.2f;
    private GameObject grabObj;
    RaycastHit2D hit;


    [SerializeField] float X_throwAmount = 1f;
    [SerializeField] float Y_throwAmount = 1f;
    [SerializeField] float X_kickAmount = 1f;
    [SerializeField] float Y_kickAmount = 1f;

    [SerializeField] GameObject meter;
    [SerializeField] float forceAmount = 1f;

    MoveCharacterAction _moveCharacterAction;

    [SerializeField] ParticleSystem KickedParticleSystem;

    //private bool meter_isOn = false;

    private bool changeRotate;
    private float rotate;
    private float angle;
    private float currentDegree;
    [SerializeField] float initDegree = 0f;

    PlayerController_yy _playerController_yy;

    private bool kickFlag = false;

    [SerializeField] AudioClip kickSE;
    [SerializeField] AudioClip throwSE;
    private AudioSource _audioSource;

    private void Start()
    {
        _moveCharacterAction = GetComponent<MoveCharacterAction>();
        _playerController_yy = GetComponent<PlayerController_yy>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        

        if (Input.GetKey(KeyCode.X))
        {
            if (grabObj == null)
            {
                if (isRight())
                {
                    hit = Physics2D.CircleCast(this.gameObject.transform.position + rayPoint.localPosition, rayRadius, transform.right, rayDistance);
                }
                else
                {
                    hit = Physics2D.CircleCast(this.gameObject.transform.position + rayPointForLeft.localPosition, rayRadius, -transform.right, rayDistance);
                }


                if (hit.collider != null && (hit.collider.tag == "PresentBox" || hit.collider.tag == "ParachuteBox"))
                {
                    //持つ
                    grabObj = hit.collider.gameObject;
                    grabObj.GetComponent<Rigidbody2D>().isKinematic = true;
                    // X軸とY軸の位置を固定する
                    grabObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                    // Z軸の回転を固定する
                    grabObj.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezeRotation;

                    grabObj.GetComponent<BoxCollider2D>().enabled = false;
                    grabObj.transform.position = grabPoint.position;
                    grabObj.transform.SetParent(transform);

                    _playerController_yy.HaveBoxAnim();
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            if (grabObj != null)
            {
                //投げる
                grabObj.GetComponent<Rigidbody2D>().isKinematic = false;
                // 全ての制約を解除する
                grabObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                grabObj.GetComponent<BoxCollider2D>().enabled = true;
                grabObj.transform.SetParent(null);
                Rigidbody2D rb = grabObj.GetComponent<Rigidbody2D>();

                if (isRight())
                {
                    rb.AddForce(new Vector2(1f * X_throwAmount, 1f * Y_throwAmount), ForceMode2D.Impulse);
                    //rb.velocity = new Vector2(1f * X_throwAmount, 1f * Y_throwAmount) * Time.deltaTime * forceAmount;
                }
                else
                {
                    rb.AddForce(new Vector2(-1f * X_throwAmount, 1f * Y_throwAmount), ForceMode2D.Impulse);
                    //rb.velocity = new Vector2(1f * X_throwAmount, 1f * Y_throwAmount) * Time.deltaTime * forceAmount;
                }

                if (grabObj.tag == "ParachuteBox")
                {
                    grabObj.GetComponent<ParachuteBoxController>().parachuteThrown = true;
                }

                _playerController_yy.ThrowBoxAnim();
                _audioSource.PlayOneShot(throwSE);
                grabObj = null;
            }
            
        }
        else
        {
            _playerController_yy.ThrowBoxAnimFalse();

            if (isRight())
            {
                hit = Physics2D.CircleCast(this.gameObject.transform.position + rayPoint.localPosition, rayRadius, transform.right, rayDistance);
            }
            else
            {
                hit = Physics2D.CircleCast(this.gameObject.transform.position + rayPointForLeft.localPosition, rayRadius, -transform.right, rayDistance);
            }

            if (hit.collider != null && hit.collider.gameObject.tag == "PresentBox")
            {
                if (!(kickFlag))
                {
                    //蹴る
                    float diff = hit.collider.gameObject.transform.position.x - this.gameObject.transform.position.x;
                    Rigidbody2D rb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    //エフェクト
                    Instantiate(KickedParticleSystem, hit.point, Quaternion.identity);
                    //効果音
                    _audioSource.PlayOneShot(kickSE);

                    if (diff > 0 && isRight())
                    {
                        rb.AddForce(new Vector2(1f * X_kickAmount, 1f * Y_kickAmount), ForceMode2D.Force);
                        //rb.velocity = new Vector2(1f * X_kickAmount, 1f * Y_kickAmount) * Time.deltaTime * forceAmount;
                    }
                    else if (diff < 0 && !(isRight()))
                    {
                        rb.AddForce(new Vector2(-1f * X_kickAmount, 1f * Y_kickAmount), ForceMode2D.Force);
                        //rb.velocity = new Vector2(-1f * X_kickAmount, 1f * Y_kickAmount) * Time.deltaTime * forceAmount;
                    }

                    _playerController_yy.KickAnim();
                    kickFlag = true;
                }
                
            }
            else
            {
                kickFlag = false;
                _playerController_yy.KickAnimFalse();
            }
        }
    }

    bool isRight()
    {
        if (!GetComponent<SpriteRenderer>().flipX)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.gameObject.transform.position + rayPoint.localPosition, rayRadius);
        Gizmos.DrawWireSphere(this.gameObject.transform.position + rayPoint.localPosition + transform.right * rayDistance, rayRadius);

        Gizmos.DrawWireSphere(this.gameObject.transform.position + rayPointForLeft.localPosition, rayRadius);
        Gizmos.DrawWireSphere(this.gameObject.transform.position + rayPointForLeft.localPosition - transform.right * rayDistance, rayRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void MoveMeter()
    {
        //角度が0.1度以下になるとtureになる。0度は360度
        if (0.1f >= meter.transform.eulerAngles.z)
        {
            changeRotate = true;
        }
        //90度以上になるとfalseに切り替わる
        if (90 <= meter.transform.eulerAngles.z)
        {
            changeRotate = false;
        }

        //trueなら角度を1足す、falseなら-1足す
        if (changeRotate)
        {
            rotate = 1;
        }
        else
        {
            rotate = -1;
        }

        currentDegree += rotate;


        if (isRight())
        {
            meter.transform.localEulerAngles = new Vector3(0f, 0f, currentDegree);
            //Debug.Log((currentDegree, currentDegree + 90f));
        }
        else
        {
            meter.transform.localEulerAngles = new Vector3(0f, 0f, currentDegree + 90f);
            //Debug.Log((currentDegree, currentDegree + 90f));
        }
    }
}
