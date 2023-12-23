using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonGimicController : MonoBehaviour
{
    [SerializeField] Sprite sprite_1;
    [SerializeField] Sprite sprite_2;
    [SerializeField] Sprite sprite_3;

    [SerializeField] GameObject presentBoxes;
    [SerializeField] GameObject gimic_image;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PresentBox")
        {
            gimic_image.GetComponent<SpriteRenderer>().enabled = false;
            presentBoxes.SetActive(true);
        }
    }
}
