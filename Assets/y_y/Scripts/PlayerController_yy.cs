using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController_yy : MonoBehaviour
{
    [SerializeField] LayerMask groundMask;
    [SerializeField, HideInInspector] Rigidbody2D rig2d;
    [SerializeField, HideInInspector] SpriteRenderer spriteRenderer;
    [SerializeField, HideInInspector] Animator animator;
    [SerializeField] float characterHeightOffset;

    [SerializeField] float test;

    public bool havingBox;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rig2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float axis = Input.GetAxis("Horizontal");
        bool isDown = Input.GetAxisRaw("Vertical") < 0;



        Vector2 velocity = rig2d.velocity;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("jump", true);
            velocity.y = 5;
        }
        if (axis != 0)
        {
            animator.SetBool("run", true);
            spriteRenderer.flipX = axis < 0;
            velocity.x = axis * 2;
        }
        else
        {
            animator.SetBool("run", false);
        }
        rig2d.velocity = velocity;

        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //6 = Ground
        if(collision.gameObject.layer == 6)
        {
            animator.SetBool("jump", false);
        }
    }

    public void HaveBoxAnim()
    {
        animator.SetBool("havebox", true);
    }

    public void ThrowBoxAnim()
    {
        animator.SetBool("throw", true);
        animator.SetBool("havebox", false);
    }

    public void ThrowBoxAnimFalse()
    {
        animator.SetBool("throw", false);
    }

    public void KickAnim()
    {
        animator.SetBool("kick", true);
    }

    public void KickAnimFalse()
    {
        animator.SetBool("kick", false);
    }
}