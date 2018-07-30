using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScript : MonoBehaviour {

	// Use this for initialization
    public enum Direction
    {
        Right,
        Left,
        Forward,
        Back,
        Down
    };
    Vector3 localRight;
    Vector3 localLeft;
    Vector3 localForward;
    Vector3 localBack;
    Vector3 localDown;

    public Direction rayDirection;
    Ray localRay;
    Vector3 localDirection;
    public bool hittingWall;  
    void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        localForward = transform.parent.InverseTransformDirection(transform.forward);
        localRight = transform.parent.InverseTransformDirection(transform.right);
        localLeft = localRight * -1;
        localBack = localForward * -1;
        localDown = transform.parent.InverseTransformDirection(transform.up) * -1;
        
        switch (rayDirection)
        {
            case Direction.Right:
                localRay = new Ray(transform.localPosition, localRight);
                localDirection = localRight;
                break;

            case Direction.Left:
                localRay = new Ray(transform.localPosition, localLeft);
                localDirection = localLeft;
                break;

            case Direction.Forward:
                localRay = new Ray(transform.localPosition, localForward);
                localDirection = localForward;
                break;

            case Direction.Back:
                localRay = new Ray(transform.localPosition, localBack);
                localDirection = localBack;
                break;
            case Direction.Down:
                localRay = new Ray(transform.localPosition, localBack);
                localDirection = localDown;
                break;

        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(localDirection), out hit, 0.2f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(localDirection) * hit.distance, Color.red);
            hittingWall = true;
        }
        else
        {
            hittingWall = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(localDirection) * .2f, Color.white);
        }

    }
}
