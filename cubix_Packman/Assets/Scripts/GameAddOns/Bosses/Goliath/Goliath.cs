using System;
using System.Collections.Generic;
using UnityEngine;

public class Goliath : MonoBehaviour
{
    private float _initialTimer = 0;
    private float WaitinhTIme = 5f;

    public static List<Transform> PossiblePositions = new List<Transform>();

    public static Vector3 InitialPosition;

    public static Vector3 Destination;
    public static Vector3 LocalDestination;

    private float _tolarance = 0.001f;

    private float _speed = 3.5f;

    private bool _stepFlag = true;



    Vector3 _localRight;
    Vector3 _localForward;

    private bool _atJuntion = true;

    private void Start()
    {
        Destination = transform.localPosition;
        LocalDestination = transform.localPosition;
    }

    private void FixedUpdate()
    {

        _localForward = transform.parent.InverseTransformDirection(transform.up) * 2;
        _localRight = transform.parent.InverseTransformDirection(transform.right) * 2;

        if (!_atJuntion)
        {
            Move();

        }
        else
        {
            Junction();

        }

    }

    void Move()
    {
        //move to the destination

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, LocalDestination, _speed * Time.deltaTime);

        if ((transform.localPosition - LocalDestination).magnitude < _tolarance)
        {
            transform.localPosition = LocalDestination;
            _atJuntion = true;
        }
    }

    void Junction()
    {
        //update the destination to nearest local destination
        Vector3 tempRight = transform.localPosition + _localRight;
        Vector3 tempLeft = transform.localPosition - _localRight;
        Vector3 tempForward = transform.localPosition + _localForward;
        Vector3 tempBack = transform.localPosition - _localForward;

        List<Vector3> TempPosList = new List<Vector3>();
        foreach (var tran in PossiblePositions)
        {
            TempPosList.Add(tran.localPosition);
        }

        float tempMag = (Destination - transform.localPosition).magnitude;

        if (Contains(tempRight, TempPosList))
        {
            Debug.Log("here 1");
            if ((tempRight - Destination).magnitude <= tempMag)
            {
                LocalDestination = tempRight;
                tempMag = (tempRight - Destination).magnitude;
            }
        }

        if (Contains(tempLeft, TempPosList))
        {
            Debug.Log("here 2");
            if ((tempLeft - Destination).magnitude <= tempMag)
            {
                LocalDestination = tempLeft;
                tempMag = (tempLeft - Destination).magnitude;
            }
        }

        if (Contains(tempForward, TempPosList))
        {
            Debug.Log("here 3");
            if ((tempForward - Destination).magnitude <= tempMag)
            {
                LocalDestination = tempForward;
                tempMag = (tempForward - Destination).magnitude;
            }
        }

        if (Contains(tempBack, TempPosList))
        {
            Debug.Log("here 4");
            if ((tempBack - Destination).magnitude <= tempMag)
            {
                LocalDestination = tempBack;
            }
        }

        _atJuntion = false;

    }

    bool Contains(Vector3 toCheck, List<Vector3> ListOfVectors)
    {
        foreach (var vector in ListOfVectors)
        {
            if (Math.Abs(vector.x - toCheck.x) < _tolarance && Math.Abs(vector.y - toCheck.y) < _tolarance && Math.Abs(vector.z - toCheck.z) < _tolarance)
                return true;
        }

        return false;
    }
}






