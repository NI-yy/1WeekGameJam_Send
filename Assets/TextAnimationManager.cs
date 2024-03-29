using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimationManager : MonoBehaviour
{
    [SerializeField] TextMeshProGeometryAnimator Clear_text;
    [SerializeField] TextMeshProGeometryAnimator Score;
    [SerializeField] TextMeshProGeometryAnimator Score_text;
    [SerializeField] TextMeshProGeometryAnimator Thanks_text;
    float delay = 0;

    void Update()
    {
        Clear_text.progress += Time.unscaledDeltaTime / 2;
        if (Clear_text.progress > 1 ) Clear_text.progress = 0;

        if(Score != null)
        {
            Score.progress += Time.unscaledDeltaTime / 1.5f;
        }

        if(Thanks_text != null)
        {
            Thanks_text.progress += Time.unscaledDeltaTime / 1.5f;
        }
        

        delay += Time.unscaledDeltaTime;
        if (delay > 0.5)
        {
            if (Score_text != null)
            {
                Score_text.progress += Time.unscaledDeltaTime / 2;
            }
            
        }
    }
}
