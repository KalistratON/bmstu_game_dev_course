using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator myAnimator;

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            myAnimator.SetTrigger("Jump");
        }
    }
}
