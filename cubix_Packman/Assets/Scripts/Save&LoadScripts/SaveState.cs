using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using UnityEngine;


[Serializable]
public class SaveState {

    public float levelSize;
    public Dictionary<int, string> PortalPairs;
    public List<SaveavleNode> node;
}

public class Node
{
    public PrefabType Type;

    public Transform transform;
}

[Serializable]
public class SaveavleNode
{
    //ID
    public PrefabType Type;

    //transform.position
    public float posx;
    public float posy;
    public float posz;

    //transform.rotation
    public float rotx;
    public float roty;
    public float rotz;

    public void ConvertToSaveable( PrefabType type, Transform transform)
    {
        this.Type = type;

        float temp_posx = transform.position.x;
        float temp_posy = transform.position.y;
        float temp_posz = transform.position.z;
        float truncatedx = (float)(Math.Truncate((double)temp_posx*100.0) / 100.0);
        float truncatedy = (float)(Math.Truncate((double)temp_posx*100.0) / 100.0);
        float truncatedz = (float)(Math.Truncate((double)temp_posx*100.0) / 100.0);
        this.posx  = (float)(Math.Round((double)temp_posx, 2));
        this.posy  = (float)(Math.Round((double)temp_posy, 2));
        this.posz  = (float)(Math.Round((double)temp_posz, 2));

        this.posx = transform.position.x;
        this.posy = transform.position.y;
        this.posz = transform.position.z;

        Vector3 rot = transform.eulerAngles;
        
/*
        float temp_rotx = rot.x;
        float temp_roty = rot.y;
        float temp_rotz = rot.z;
        float truncatedrotx = (float)(Math.Truncate((double)temp_rotx*100.0) / 100.0);
        float truncatedroty = (float)(Math.Truncate((double)temp_roty*100.0) / 100.0);
        float truncatedrotz = (float)(Math.Truncate((double)temp_rotz*100.0) / 100.0);
        this.posx  = (float)(Math.Round((double)temp_posx, 2));
        this.posy  = (float)(Math.Round((double)temp_posy, 2));
        this.posz  = (float)(Math.Round((double)temp_posz, 2));
*/
        
        this.rotx = rot.x;
        this.roty = rot.y;
        this.rotz = rot.z;
    }
    public Node ConvertToNode()
    {
        Node node = new Node();

        node.Type = this.Type;

        GameObject tempObj = new GameObject();
        node.transform = tempObj.transform;

        node.transform.position = new Vector3(this.posx, this.posy, this.posz);

        Quaternion rotation = Quaternion.Euler(this.rotx, this.roty, this.rotz);
        node.transform.rotation = rotation;
        return node;
    }
}