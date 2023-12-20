using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestTaiyo : MonoBehaviour
{
    Animator anim;

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

        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("kick", true);
        }
        else
        {
            anim.SetBool("kick", false);
        }
    }
}
