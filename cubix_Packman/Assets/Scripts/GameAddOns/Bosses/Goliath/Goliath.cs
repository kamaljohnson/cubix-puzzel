using System.Collections.Generic;
using UnityEngine;

public class Goliath : MonoBehaviour
{
    public static float ResponseTime = 1f;
 
    public static float ResponseTimer = ResponseTime;
    
    public static bool TriggerFlag;
    public static List<Vector3> PossiblePositions = new List<Vector3>();

    public static Vector3 InitialPosition;
    public static Vector3 Destination;
    public static Vector3 TempDestination;
    private Vector3 _intermediateDestination;

    private float _tolarance = 0.4f;

    private float _speed = 4f;

    public static bool MovementFlag;

    private bool _stepFlag = true;
    Vector3 _offset = new Vector3(0, 0, .9f);
    
    private void Awake()
    {
        MovementFlag = false;
        Destination = TempDestination;
    }

    private void Update()
    {
        if (ResponseTimer > ResponseTime)
        {
            if (TempDestination != Destination)
            {
                Destination = TempDestination;
                ResponseTimer = ResponseTime;
            }
        }
        else
        {
            ResponseTimer += Time.deltaTime;
        }

        Debug.Log("timer : " + ResponseTimer.ToString());
        if (_stepFlag)
        {
            var temp = Destination - transform.localPosition;
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





















