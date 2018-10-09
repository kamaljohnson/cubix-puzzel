using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Key : MonoBehaviour
{

    public GameObject Lock;

    public void ActivateKey()
    {
        if (Lock.activeSelf == true)
        {
            if (!CheckPoint.LocksTemp.Contains(Lock))
            {
                CheckPoint.LocksTemp.Add(Lock);
            }
        }
        else
        {
            CheckPoint.LocksTemp.Remove(Lock);
        }
        Lock.SetActive(!Lock.activeSelf);
    }

}
