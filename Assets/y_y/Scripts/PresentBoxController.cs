using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PresentBoxController : MonoBehaviour
{
    public float rayDistance = 0.12f;
    [SerializeField] LayerMask groundMask;
    RaycastHit2D hit;
    public bool kicked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red, 0.1f, false);

        if (OnGround())
        {
            kicked = false;
        }
    }

    public bool OnGround()
    {
        hit = Physics2D.Raycast(transform.position, Vector3.down, rayDistance, groundMask);
        return hit.collider != null;
    }
}
