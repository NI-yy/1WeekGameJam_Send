using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestTaiyo : MonoBehaviour
{
    Animator anim;
    bool haveBox;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("kick", true);
        }
        else
        {
            anim.SetBool("kick", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!haveBox)
            {
                haveBox = true;
                anim.SetBool("havebox", true);
            }
            else
            {
                haveBox = false;
                anim.SetBool("throw", true);
                anim.SetBool("havebox", false);
            }
        }
        else 
        {
            anim.SetBool("throw", false);
        }
    }
}
