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

    //private bool meter_isOn = false;

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

        if (Input.GetKeyDown(KeyCode.X))
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
                    grabObj = hit.collider.gameObject;
                    grabObj.GetComponent<Rigidbody2D>().isKinematic = true;
                    grabObj.transform.position = grabPoint.position;
                    grabObj.transform.SetParent(transform);
                }
            }
            else
            {
                //投げる
                grabObj.GetComponent<Rigidbody2D>().isKinematic = false;
                grabObj.transform.SetParent(null);
                Rigidbody2D rb = grabObj.GetComponent<Rigidbody2D>();

                if (isRight())
                {
                    rb.AddForce(new Vector2(1f * X_throwAmount, 1f * Y_throwAmount), ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector2(-1f * X_throwAmount, 1f * Y_throwAmount), ForceMode2D.Impulse);
                }

                if(grabObj.tag == "ParachuteBox")
                {
                    grabObj.GetComponent<ParachuteBoxController>().parachuteThrown = true;
                }

                grabObj = null;
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
        if (collision.gameObject.tag == "PresentBox")
        {
            float diff = collision.gameObject.transform.position.x - this.gameObject.transform.position.x;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (diff > 0 && isRight())
            {
                rb.AddForce(new Vector2(1f * X_kickAmount, 1f * Y_kickAmount), ForceMode2D.Impulse);
            }
            else if (diff < 0 && !(isRight()))
            {
                rb.AddForce(new Vector2(-1f * X_kickAmount, 1f * Y_kickAmount), ForceMode2D.Impulse);
            }
        }
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
