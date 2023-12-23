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

    //private bool meter_isOn = false;

    private bool changeRotate;
    private float rotate;
    private float angle;
    private float currentDegree;
    [SerializeField] float initDegree = 0f;

    PlayerController_yy _playerController_yy;

    private bool kickFlag = false;

    private void Start()
    {
        _moveCharacterAction = GetComponent<MoveCharacterAction>();
        _playerController_yy = GetComponent<PlayerController_yy>();
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

                Debug.Log(hit.collider.gameObject);

                if (hit.collider != null && (hit.collider.tag == "PresentBox" || hit.collider.tag == "ParachuteBox"))
                {
                    //����
                    grabObj = hit.collider.gameObject;
                    grabObj.GetComponent<Rigidbody2D>().isKinematic = true;
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
                //������
                grabObj.GetComponent<Rigidbody2D>().isKinematic = false;
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
                    //�R��
                    float diff = hit.collider.gameObject.transform.position.x - this.gameObject.transform.position.x;
                    Rigidbody2D rb = hit.collider.gameObject.GetComponent<Rigidbody2D>();

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
        //�p�x��0.1�x�ȉ��ɂȂ��ture�ɂȂ�B0�x��360�x
        if (0.1f >= meter.transform.eulerAngles.z)
        {
            changeRotate = true;
        }
        //90�x�ȏ�ɂȂ��false�ɐ؂�ւ��
        if (90 <= meter.transform.eulerAngles.z)
        {
            changeRotate = false;
        }

        //true�Ȃ�p�x��1�����Afalse�Ȃ�-1����
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
