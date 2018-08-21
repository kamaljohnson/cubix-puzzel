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
    public static Vector3 IntermediateDestination;

    private float _tolarance = 0.4f;

    private float _speed = 3.5f;

    public static bool MovementFlag;

    private bool _stepFlag = true;
    
    private void Start()
    {
        MovementFlag = false;
        Destination = transform.localPosition;
        TempDestination = transform.localPosition;
        IntermediateDestination = transform.localPosition;
    }

    private void Update()
    {
        if (ResponseTimer > ResponseTime)
        {
            Destination = TempDestination;
            ResponseTimer = ResponseTime;
        }
        else
        {
            ResponseTimer += Time.deltaTime;
        }

        if (_stepFlag)
        {
            var temp = Destination - transform.localPosition;
            if (temp.magnitude > _tolarance)
            {
                if (Mathf.Abs(temp.x) > _tolarance)
                {
                    IntermediateDestination = 
                        new Vector3(transform.localPosition.x + 1f * Mathf.Sign(temp.x),
                            transform.localPosition.y, transform.localPosition.z);
                    _stepFlag = false;
                }
                else if (Mathf.Abs(temp.y) > _tolarance) 
                {
                    IntermediateDestination = new Vector3(transform.localPosition.x,
                        transform.localPosition.y + 1f * Mathf.Sign(temp.y),
                        transform.localPosition.z);
                    _stepFlag = false;
                }
                else if (Mathf.Abs(temp.z) > _tolarance)
                {
                    IntermediateDestination = new Vector3(transform.localPosition.x, transform.localPosition.y,
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
        if((transform.localPosition - IntermediateDestination).magnitude > 0.001)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, IntermediateDestination, _speed * Time.deltaTime);
        else
        {
            _stepFlag = true;
        }
    }
}





















