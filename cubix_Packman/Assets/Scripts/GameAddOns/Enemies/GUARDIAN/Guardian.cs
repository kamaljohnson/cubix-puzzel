using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour
{
    
    public List<Transform> GuardianPath = new List<Transform>();
    private int _currentGuardianPositionIndex = 0;
    private int _movementDirection = 1;
    float GuardianSpeed = 1f;
    bool CanMoveToLocation = true;
    private bool _destinationReached = true;

    private float TOLORENCE = 0.001f;

    private bool TimerFlag = false;
    private float Timer = 0;
    private float GardianWaitingTimer = 5;
    private void Update()
    {
        if (TimerFlag)
        {
            Timer += Time.deltaTime * 10;
            if (Timer > GardianWaitingTimer)
            {
                TimerFlag = false;
                Timer = 0;
            }
        }
        if (_destinationReached)
        {
            _destinationReached = false;
            //check if possible to move to the next index via _movementDirection
            
            if (CanMoveToLocation)
            {
                if ((_currentGuardianPositionIndex == 0 && _movementDirection == -1) || (_currentGuardianPositionIndex == GuardianPath.Count - 1 && _movementDirection == 1))
                {
                    TimerFlag = true;
                    _movementDirection *= -1;
                }
                _currentGuardianPositionIndex += _movementDirection;

            }
            else
            {
                TimerFlag = true;
                _movementDirection *= -1;
            }
        }
        
        if(!TimerFlag)
            Move();
    }

    private void Move()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, GuardianPath[_currentGuardianPositionIndex].localPosition, Time.deltaTime * GuardianSpeed/* * animationSpeed*/);
        if ((transform.localPosition - GuardianPath[_currentGuardianPositionIndex].localPosition).magnitude < TOLORENCE)
        {
            Debug.Log("current -> " + _currentGuardianPositionIndex.ToString() + "transfrom : " + transform.localPosition.ToString() + " : " + GuardianPath[_currentGuardianPositionIndex].localPosition.ToString());
            _destinationReached = true;
        }
    }
}