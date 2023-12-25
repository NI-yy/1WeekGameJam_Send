using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackWallController : MonoBehaviour
{
    [SerializeField] GameObject presentBox;
    [SerializeField] ParticleSystem kirakiraEffect;
    public bool generatePresent = false;
    // Start is called before the first frame update
    void Start()
    {
        if (generatePresent)
        {
            Instantiate(kirakiraEffect, transform.position, Quaternion.identity, this.transform);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PresentBox")
        {
            if (generatePresent)
            {
                Instantiate(presentBox, transform.position, Quaternion.identity);
                Instantiate(presentBox, transform.position, Quaternion.identity);
                Instantiate(presentBox, transform.position, Quaternion.identity);
            }
            

            Destroy(this.gameObject);
        }
    }
}
