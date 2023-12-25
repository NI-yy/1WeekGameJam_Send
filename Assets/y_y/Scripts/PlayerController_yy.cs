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
    [SerializeField] ParticleSystem jumpParticleSystem;
    [SerializeField] ParticleSystem landingParticleSystem;
    [SerializeField] private Transform footPoint;
    [SerializeField] AudioClip jumpSE;
    [SerializeField] AudioClip landingSE;

    private AudioSource _audioSource;

    public bool havingBox;
    [SerializeField] float rayLength = 1f;
    private bool midAir = false;

    [SerializeField] float dash = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rig2d = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
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
            Instantiate(jumpParticleSystem, footPoint.position, Quaternion.identity);
            _audioSource.PlayOneShot(jumpSE);

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


        //着地判定のためのraycast
        var hit = Physics2D.Raycast(transform.position, Vector3.down, rayLength, groundMask);
        if(hit.collider == null)
        {
            if (midAir)
            {
                animator.SetBool("jump", true);
            }
            midAir = true;
        }
        else
        {
            if (midAir)
            {
                _audioSource.PlayOneShot(landingSE);
            }

            midAir = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //6 = Ground
        if (collision.gameObject.layer == 6)
        {
            animator.SetBool("jump", false);
            //着地以外でもエフェクトが出てしまうが、走ってる感じがあってむしろ良いので残す
            Instantiate(landingParticleSystem, footPoint.position, Quaternion.identity);
            //_audioSource.PlayOneShot(landingSE);
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

    public void HaveBoxAnimFalse()
    {
        animator.SetBool("havebox", false);
    }

    public void KickAnim()
    {
        animator.SetBool("kick", true);
    }

    public void KickAnimFalse()
    {
        animator.SetBool("kick", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.down * rayLength);
    }
}