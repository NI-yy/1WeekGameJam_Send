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

    MoveCharacterAction _moveCharacterAction;

    private bool meter_isOn = false;

    private bool changeRotate;
    private float rotate;
    private float angle;
    private float currentDegree;
    [SerializeField] float initDegree = 0f;

    private void Start()
    {
        _moveCharacterAction = GetComponent<MoveCharacterAction>();
    }

    void Update()
    {

        if (meter_isOn)
        {
            MoveMeter();
        }

        if (Input.GetKeyDown(KeyCode.L))
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

                
                
                if (hit.collider != null && hit.collider.tag == "PresentBox")
                {
                    grabObj = hit.collider.gameObject;
                    grabObj.GetComponent<Rigidbody2D>().isKinematic = true;
                    grabObj.transform.position = grabPoint.position;
                    grabObj.transform.SetParent(transform);


                    //if (grabObj.GetComponent<PresentBoxController>().OnGround())
                    //{
                    //    //�����ڒn���Ă���Ƃ��ɂ��Ă�
                    //    grabObj.GetComponent<Rigidbody2D>().isKinematic = true;
                    //    grabObj.transform.position = grabPoint.position;
                    //    grabObj.transform.SetParent(transform);

                    //    _moveCharacterAction.isHolding = true;
                    //}
                    //else
                    //{
                    //    //�����ڒn���Ă��炸�A���R���Ă��Ȃ��Ƃ��R���
                    //    if (!(grabObj.GetComponent<PresentBoxController>().kicked))
                    //    {
                    //        Rigidbody2D rb = grabObj.GetComponent<Rigidbody2D>();
                    //        if (isRight())
                    //        {
                    //            rb.AddForce(new Vector2(1f * X_kickAmount, 1f * Y_kickAmount), ForceMode2D.Impulse);
                    //        }
                    //        else
                    //        {
                    //            rb.AddForce(new Vector2(-1 * X_kickAmount, 1f * Y_kickAmount), ForceMode2D.Impulse);
                    //        }

                    //        grabObj.GetComponent<PresentBoxController>().kicked = true;
                    //    }

                    //    grabObj = null;
                    //}


                }
            }
            else
            {
                if (meter_isOn)
                {
                    //������
                    grabObj.GetComponent<Rigidbody2D>().isKinematic = false;
                    grabObj.transform.SetParent(null);
                    Rigidbody2D rb = grabObj.GetComponent<Rigidbody2D>();

                    if (isRight())
                    {
                        rb.AddForce(new Vector2(Mathf.Cos(currentDegree * Mathf.Deg2Rad) * X_throwAmount, Mathf.Sin(currentDegree * Mathf.Deg2Rad) * Y_throwAmount), ForceMode2D.Impulse);
                        Debug.Log((currentDegree, Mathf.Deg2Rad, currentDegree * Mathf.Deg2Rad));
                        Debug.Log((Mathf.Cos(currentDegree * Mathf.Deg2Rad), Mathf.Sin(currentDegree * Mathf.Deg2Rad)));
                    }
                    else
                    {
                        rb.AddForce(new Vector2(-Mathf.Cos(currentDegree * Mathf.Deg2Rad) * X_throwAmount, Mathf.Sin(currentDegree * Mathf.Deg2Rad) * Y_throwAmount), ForceMode2D.Impulse);
                    }

                    grabObj = null;

                    meter.SetActive(false);
                    meter_isOn = false;
                }
                else
                {
                    meter.SetActive(true);
                    meter.transform.localEulerAngles = new Vector3(0f, 0f, initDegree);

                    currentDegree = initDegree;
                    meter_isOn = true;
                }
                
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
        if(collision.gameObject.tag == "PresentBox")
        {
            float diff = collision.gameObject.transform.position.x - this.gameObject.transform.position.x;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (diff > 0 && isRight())
            {
                rb.AddForce(new Vector2(1f * X_kickAmount, 1f * Y_kickAmount), ForceMode2D.Impulse);
            }
            else if(diff < 0 && !(isRight()))
            {
                rb.AddForce(new Vector2(-1f * X_kickAmount, 1f * Y_kickAmount), ForceMode2D.Impulse);
            }
        }
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