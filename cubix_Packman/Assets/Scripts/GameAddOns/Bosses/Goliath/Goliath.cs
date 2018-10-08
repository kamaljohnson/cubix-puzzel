using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Goliath : MonoBehaviour
{
    
    public static bool EditorMode = false;
    public static float health;
    public static float MaxHealth = 20;
    private static float damage = 20;
    
    private float _initialTimer = 0;
    private float WaitinhTIme = 5f;

    public static List<Transform> PossiblePositions = new List<Transform>();

    public static Vector3 InitialPosition;

    public static Vector3 Destination;
    public static Vector3 LocalDestination;

    private float _tolarance = 0.001f;

    private float _speed = 1.35f;

    private bool _stepFlag = true;

    Vector3 _localRight;
    Vector3 _localForward;

    private float WaitingTimer = 0;
    private float WaitingTime = 2;
    
    private GoliathAnimationScript anim;
    public GameObject GoliathMesh;
    
    private bool _atJuntion = true;

    private void Start()
    {
        
        gameObject.SetActive(true);
        GameManager.EndState.SetActive(false);
        health = MaxHealth;
        anim = GoliathMesh.GetComponent<GoliathAnimationScript>();

        Destination = transform.localPosition;
        LocalDestination = transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            GameManager.EndState.SetActive(true);
            gameObject.SetActive(false);
        }

        if (Destination == transform.localPosition)
        {
            WaitingTimer+=Time.deltaTime;
            if (WaitingTimer >= WaitingTime)
            {
                Random rnd = new Random();
                Destination = PossiblePositions[rnd.Next(0, PossiblePositions.Count)].localPosition;
                WaitingTimer = 0;
            }
        }
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
            if ((tempRight - Destination).magnitude <= tempMag)
            {
                LocalDestination = tempRight;
                tempMag = (tempRight - Destination).magnitude;
                anim.right = true;
            }
        }

        if (Contains(tempLeft, TempPosList))
        {
            if ((tempLeft - Destination).magnitude <= tempMag)
            {
                LocalDestination = tempLeft;
                tempMag = (tempLeft - Destination).magnitude;
                anim.left = true;
            }
        }

        if (Contains(tempForward, TempPosList))
        {
            if ((tempForward - Destination).magnitude <= tempMag)
            {
                LocalDestination = tempForward;
                tempMag = (tempForward - Destination).magnitude;
                anim.forward = true;
            }
        }

        if (Contains(tempBack, TempPosList))
        {
            if ((tempBack - Destination).magnitude <= tempMag)
            {
                LocalDestination = tempBack;
                anim.back = true;
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

    public static void Atacked()
    {
        health -= damage;

    }
}






