using Boo.Lang;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public static Vector3 MazeCurrentCheckPointTransformRotation;
    public static Vector3 PlayerCurrentCheckPointTransformPosition;
    
    public static List<GameObject> LocksActive = new List<GameObject>();
    public static List<GameObject> LocksTemp = new List<GameObject>();

    public static void SetCheckPointTransfrom(Vector3 checkpoinPosition)
    {
        Debug.Log("checkpoint set");
        LocksActive = new List<GameObject>();

        foreach (var L in LocksTemp)
        {
            LocksActive.Add(L);
        }
        MazeCurrentCheckPointTransformRotation = FindObjectOfType<playerController>().MazeBody.transform.eulerAngles;
        PlayerCurrentCheckPointTransformPosition = checkpoinPosition;
    }

    public static void ActivateCheckPoint()
    {
        Debug.Log("activated the checkpoint");
        
        foreach(var k in FindObjectsOfType<Key>())
        {
            if (LocksActive.Count == 0)
            {
                k.Lock.SetActive(true);
                continue;
            }

            if(LocksActive.Contains(k.Lock))
                k.Lock.SetActive(false);
            else
            {
                k.Lock.SetActive(true);
            }
        }
        
        FindObjectOfType<SwipeControl>().Reset();
        FindObjectOfType<playerController>().Reset();
        Player.IsAlive = true;
    }
}
