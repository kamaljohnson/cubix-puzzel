using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoliathAnimationScript : MonoBehaviour {

    // Use this for initialization
    Animator anim;
    public bool right, left, forward, back;    //triggers for each direciton 
    public bool animationStop;
    void Start ()
    {
        animationStop = false;
        anim = GetComponent<Animator>();
        right = false;
        left = false;
        forward = false;
        back = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!animationStop)
        {
            if (right)
            {
                anim.Play("GoliathRightRolling", -1, 0f);
                Debug.Log("Right");
            }
            else if (left)
            {
                anim.Play("GoliathLeftRolling", -1, 0f);
                Debug.Log("Left");
            }
            else if (forward)
            {
                anim.Play("GoliathForwardRolling", -1, 0f);
                Debug.Log("Forward");
            }
            else if (back)
            {
                anim.Play("GoliathBackRolling", -1, 0f);
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
*/
    }
}
