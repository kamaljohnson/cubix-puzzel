using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public static Vector3 MazeCurrentCheckPointTransformRotation;
    public static Vector3 PlayerCurrentCheckPointTransformRotation;
    public static Vector3 PlayerCurrentCheckPointTransformPosition;
    

    public static void SetCheckPointTransfrom(Vector3 checkpoinPosition)
    {
        Debug.Log("checkpoint set");
        MazeCurrentCheckPointTransformRotation = FindObjectOfType<playerController>().MazeBody.transform.eulerAngles;
        PlayerCurrentCheckPointTransformRotation = Vector3.zero;
        PlayerCurrentCheckPointTransformPosition = checkpoinPosition;
    }

    public static void ActivateCheckPoint()
    {
        Debug.Log("activated the checkpoint");
        FindObjectOfType<SwipeControl>().Reset();
        FindObjectOfType<playerController>().Reset();
        Player.IsAlive = true;
    }
}
