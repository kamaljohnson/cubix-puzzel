using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goliath : MonoBehaviour
{
    public static bool TriggerFlag;
    public static List<Transform> PossiblePositions = new List<Transform>();

    public static Vector3 InitialPosition;
    public static Vector3 Destination;
    private Vector3 _intermediateDestination;

    private float _tolarance = 0.4f;

    private float _speed = 3f;

    public static bool MovementFlag;

    private bool _stepFlag = true;
    Vector3 _offset = new Vector3(0, 0, .9f);
    
    private void Awake()
    {
        MovementFlag = false;
    }

    private void Update()
    {

        if (_stepFlag)
        {
            var temp = Destination - transform.localPosition;
            Debug.Log("destination : " + _intermediateDestination.ToString());
            if (temp.magnitude > _tolarance)
            {
                if (Mathf.Abs(temp.x) > _tolarance)
                {
                    _intermediateDestination = 
                        new Vector3(transform.localPosition.x + 1f * Mathf.Sign(temp.x),
                            transform.localPosition.y, transform.localPosition.z);
                    _stepFlag = false;
                }
                else if (Mathf.Abs(temp.y) > _tolarance) 
                {
                    _intermediateDestination = new Vector3(transform.localPosition.x,
                        transform.localPosition.y + 1f * Mathf.Sign(temp.y),
                        transform.localPosition.z);
                    _stepFlag = false;
                }
                else if (Mathf.Abs(temp.z) > _tolarance)
                {
                    _intermediateDestination = new Vector3(transform.localPosition.x, transform.localPosition.y,
                        transform.localPosition.z + 1f * Mathf.Sign(temp.z));
                    _stepFlag = false;
                }
            }

        }
        if (MovementFlag)
            Move();
    }

    void Move()
    {
        if((transform.localPosition - _intermediateDestination).magnitude > _tolarance)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _intermediateDestination, _speed * Time.deltaTime);
        else
        {
            _stepFlag = true;
        }
    }
}





















