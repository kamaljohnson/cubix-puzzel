using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using UnityEngine;


[Serializable]
public class SaveState {

    public float levelSize;
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
    public PrefabType T;

    //transform.position
    public float px;
    public float py;
    public float pz;

    //transform.rotation
    public int rx;
    public int ry;
    public int rz;

    public void ConvertToSaveable( PrefabType type, Transform transform)
    {
        this.T = type;

        float temp_posx = transform.position.x;
        float temp_posy = transform.position.y;
        float temp_posz = transform.position.z;

        
        float temp_rotx = transform.eulerAngles.x;
        float temp_roty = transform.eulerAngles.y;
        float temp_rotz = transform.eulerAngles.z;

        this.px  = (float)(Math.Round((double)temp_posx, 1));
        this.py  = (float)(Math.Round((double)temp_posy, 1));
        this.pz  = (float)(Math.Round((double)temp_posz, 1));

        this.rx  = (int)(Math.Round((double)temp_rotx, 0));
        this.ry  = (int)(Math.Round((double)temp_roty, 0));
        this.rz  = (int)(Math.Round((double)temp_rotz, 0));
        
    }
    public Node ConvertToNode()
    {
        Node node = new Node();

        node.Type = this.T;

        GameObject tempObj = new GameObject();
        node.transform = tempObj.transform;

        node.transform.position = new Vector3(this.px, this.py, this.pz);

        Quaternion rotation = Quaternion.Euler(this.rx, this.ry, this.rz);
        node.transform.rotation = rotation;
        return node;
    }
}