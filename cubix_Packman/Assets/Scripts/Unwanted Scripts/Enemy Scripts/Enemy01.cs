using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : MonoBehaviour {
    Collider attackSpace;
    private void Start()
    {
        attackSpace = GetComponent<Collider>();
    }
    private void Update()
    {
    }
}
