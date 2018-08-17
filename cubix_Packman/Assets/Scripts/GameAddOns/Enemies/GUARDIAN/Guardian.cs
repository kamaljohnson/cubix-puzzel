using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour
{

    public List<Transform> GuardianPath = new List<Transform>();
    private int _currentGuardianPositionIndex;
    private int _movementDirection;
    public float GuardianSpeed = 1;
    public bool CanMoveToLocation;
    private bool _destinationReached = true;
    
    private void Update()
    {
        if (_destinationReached)
        {
            //check if possible to move to the next index via _movementDirection
        }
        
        if (CanMoveToLocation)
        {
            _currentGuardianPositionIndex += _movementDirection;
            if (_currentGuardianPositionIndex % GuardianPath.Count - 1 == 0)
            {
                _movementDirection *= -1;
            }
        }
        else
        {
            _movementDirection *= -1;
        }
        Move();
    }

    private void Move()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, GuardianPath[_currentGuardianPositionIndex].position, Time.deltaTime * GuardianSpeed/* * animationSpeed*/);
        
    }
}