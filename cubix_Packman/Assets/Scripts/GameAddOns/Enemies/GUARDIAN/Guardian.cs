using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour
{
    
    public List<Transform> GuardianPath = new List<Transform>();
    private int _currentGuardianPositionIndex = 0;
    private int _movementDirection = 1;
    float GuardianSpeed = 3;
    bool CanMoveToLocation = true;
    private bool _destinationReached = true;

    private float TOLORENCE = 0.01f;
    
    private void Update()
    {
        if (_destinationReached)
        {
            _destinationReached = false;
            //check if possible to move to the next index via _movementDirection
            Debug.Log("current index : " + _currentGuardianPositionIndex.ToString());
            
            if (CanMoveToLocation)
            {
                if ((_currentGuardianPositionIndex == 0 && _movementDirection == -1) ||
                    (_currentGuardianPositionIndex == GuardianPath.Count - 1 && _movementDirection == 1))
                {
                    _movementDirection *= -1;
                    Debug.Log("changing direction");
                }
                _currentGuardianPositionIndex += _movementDirection;
            }
            else
            {
                _movementDirection *= -1;
            }
        }
        
        Move();
    }

    private void Move()
    {
        Debug.Log("Guardian Moving");
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, GuardianPath[_currentGuardianPositionIndex].localPosition, Time.deltaTime * GuardianSpeed/* * animationSpeed*/);
        if ((transform.localPosition - GuardianPath[_currentGuardianPositionIndex].localPosition).magnitude < TOLORENCE)
        {
            Debug.Log("reached the destination");
            _destinationReached = true;
        }
    }
}