using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goliath : MonoBehaviour
{
    public static bool TriggerFlag;
    public static List<Transform> PossiblePositions = new List<Transform>();

    public static Transform InitialPosition;
    public static Transform Destination;

    private float TOLARANCE = 0.01f;

    private float speed = 1f;
    
    private void Update()
    {
        
        /*Move();*/
    }

    void Move()
    {
        if (Destination.localPosition.x - transform.localPosition.x > TOLARANCE)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(Destination.localPosition.x, transform.localPosition.y, transform.localPosition.z), speed);
        }
        else if (Destination.localPosition.y - transform.localPosition.y > TOLARANCE)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,  new Vector3(transform.localPosition.x, Destination.localPosition.y, transform.localPosition.z), speed);
        }
    }

    void Reset()
    {
        transform.localPosition = InitialPosition.localPosition;
    }
}
