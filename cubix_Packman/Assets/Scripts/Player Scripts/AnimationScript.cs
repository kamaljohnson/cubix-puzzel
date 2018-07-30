using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour {

    // Use this for initialization
    Animator anim;
    public bool right, left, forward, back;    //triggers for each direciton 
    public bool animationStop;
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (!animationStop)
        {
            if (right)
            {
                anim.Play("RightRolling", -1, 0f);
                Debug.Log("Right");
            }
            else if (left)
            {
                anim.Play("LeftRolling", -1, 0f);
                Debug.Log("Left");
            }
            else if (forward)
            {
                anim.Play("ForwardRolling", -1, 0f);
                Debug.Log("Forward");
            }
            else if (back)
            {
                anim.Play("BackRolling", -1, 0f);
                Debug.Log("Back");
            }
        }
        else
        {
            animationStop = false;
        }
        right = false;
        left = false;
        forward = false;
        back = false;

    }
}
