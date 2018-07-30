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

        this.posx = transform.position.x;
        this.posy = transform.position.y;
        this.posz = transform.position.z;

        Vector3 rot = transform.eulerAngles;
        
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