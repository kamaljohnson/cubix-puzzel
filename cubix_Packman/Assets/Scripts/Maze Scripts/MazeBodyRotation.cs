using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBodyRotation : MonoBehaviour {

    bool rotationFlag = false;
    public Vector3 rotateDirection;
    float counter = 0;
    public bool rotate;
    void FixedUpdate()
    {
        if (rotate)
        {
            if (counter < 45)
            {
                counter++;
                transform.Rotate(rotateDirection);
            }
            else
            {
                rotate = false;
                counter = 0;
            }

            //rotationFlag = true;
            
            
        }
        /*if(rotationFlag)
        {   //Rotate();
            Debug.Log(counter);
            float angle = 0;
            while (true)
            {
                angle += rotateDirection.magnitude * Time.deltaTime;
                transform.Rotate(rotateDirection * Time.deltaTime);
                if (angle >= 90)
                {
                    rotationFlag = false;
                    break;
                }
            }
        }*/
    }
    /*void Rotate()
    {
        Debug.Log(counter);
        float angle = 0;
        while (true)
        {
            angle += rotateDirection.magnitude * Time.deltaTime;
            transform.Rotate(rotateDirection * Time.deltaTime);
            if (angle >= 90)
            {
                rotationFlag = false;
                break;
            }
        }
    }*/
}
