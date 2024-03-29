﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.UI;


public enum PrefabType  // all the prefabs that goes into the game level is added here 
{
    Part01,
    Part02,
    Part03,
    Part04,
    Part05,
    KeyWall,
    KeyPortal,
    Coins,
    Start,
    End,
    Spike,
    CheckPoint,
    Guardian,
    Hammer,
    Goliath,
    GoliathWalk,
    GoliathAttackSwitch,
    Chest,
    Diamonds,
}
public class LevelEditor : MonoBehaviour {

    Vector3 RightRotation;
    Vector3 LeftRotation;
    Vector3 ForwardRotation;
    Vector3 BackRotation;
    int angle;
    bool canRotateHorizontal = true;
    bool canRotateVertical = true;
    SaveState state = new SaveState();
    public Camera mainCamera;

    public GameObject Body;
    public GameObject Maze;
    public float MazeSize = 3;
    
    public Transform tilePosition;
    List<GameObject> Tiles = new List<GameObject>();
    List<GameObject> Parts = new List<GameObject>();
    List<GameObject> MazeBody = new List<GameObject>();

    List<PrefabType> PartsTypes = new List<PrefabType>();
    

    List<Node> nodes = new List<Node>();
    float startingXpos;
    float startingYpos;
    float heightOffset;
    public bool firstTime = true;
    List<Node> NodeList = new List<Node>(); // the list of all the node in a level
    Node currentNode;   //the current node which is being manipulated 

    public List<GameObject> ListOfParts;

    public List<Material> Materials;
    
    public GameObject MazeCube;

    public GameObject Tile;
    Quaternion orientation;
    bool angleSet = false;

    public GameObject angRef;

    int index = 0;
    int noOfParts = 0;
    
    bool keyPressed;
    bool isDelete = false;
    bool scrolled = false;
    public PrefabType currentPrefab;
    GameObject currentPart;
    GameObject tempObject;
    int toDeleteIndex;
    public bool isSaving;
    SaveManager sm = new SaveManager();
    
    // variables used for specifying start and end of the maze
    public bool IsStart = false;
    public bool IsEnd = false;
    public Transform StartTransform;
    public Transform EndTransform;
    
    private void Start()
    {

    }
    private void Update()
    {
        Spikes.EditorMode = true;
        Goliath.EditorMode = true;
        ForwardRotation = Body.transform.parent.InverseTransformDirection(transform.forward);
        RightRotation = Body.transform.parent.InverseTransformDirection(transform.right);
        LeftRotation = RightRotation * -1;
        BackRotation = ForwardRotation * -1;

        ChangePart();
        if (Input.GetKeyDown("up") && canRotateHorizontal)
        {
            Maze.transform.Rotate(LeftRotation * 45);
            if (angle == 0)
            {
                canRotateVertical = false;
                angle++;
            }
            else
            {
                canRotateVertical = true;
                angle = 0;
            }
        }
        if (Input.GetKeyDown("down") && canRotateHorizontal)
        {
            Maze.transform.Rotate(RightRotation * 45);
            if (angle == 0)
            {
                canRotateVertical = false;
                angle++;
            }
            else
            {
                canRotateVertical = true;
                angle = 0;
            }
        }
        if (Input.GetKeyDown("right") && canRotateVertical)
        {
            Maze.transform.Rotate(ForwardRotation * 45);
            if (angle == 0)
            {
                canRotateHorizontal = false;
                angle++;
            }
            else
            {
                canRotateHorizontal = true;
                angle = 0;
            }
        }
        if (Input.GetKeyDown("left") && canRotateVertical)
        {
            Maze.transform.Rotate(BackRotation * 45);
            if (angle == 0)
            {
                canRotateHorizontal = false;
                angle++;
            }
            else
            {
                canRotateHorizontal = true;
                angle = 0;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            tempObject.transform.Rotate(0, 0, 90);
            orientation = tempObject.transform.rotation;
            angleSet = true;
            tempObject.GetComponent<Renderer>().material = Materials[1];
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentPrefab >= (PrefabType) 4)
                currentPrefab = PrefabType.Part01;
            else
                currentPrefab += 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentPrefab == PrefabType.Part01)
                currentPrefab = PrefabType.Part05;
            else
                currentPrefab -= 1;
        }
        if (!isSaving)
        {
            for (int i = 0; i < noOfParts; i++)
            {
                Parts[i].GetComponent<Renderer>().material = Materials[0];
            }
        }
        for (int i = 0; i < noOfParts; i++)
        {
            if ((Mathf.Round((tempObject.transform.localPosition - Parts[i].transform.localPosition).magnitude) == 0 && Mathf.Round((tempObject.transform.rotation.eulerAngles - Parts[i].transform.rotation.eulerAngles).magnitude) == 0 && currentPrefab == PartsTypes[i] && currentPrefab <= PrefabType.Part05) ||
                (Mathf.Round((tempObject.transform.localPosition - Parts[i].transform.localPosition).magnitude) == 0) && currentPrefab > PrefabType.Part05 && currentPrefab == PartsTypes[i])
            {
                isDelete = true;
                Parts[i].GetComponent<Renderer>().material = Materials[2];
                tempObject.GetComponent<Renderer>().material = Materials[2];
                toDeleteIndex = i;
                break;
            }
            isDelete = false;
            tempObject.GetComponent<Renderer>().material = Materials[1];
        }

        if (Input.GetMouseButtonDown(0) && !firstTime && canRotateVertical && canRotateHorizontal )
        {
            if (!isDelete)
            {
                tempObject.GetComponent<Renderer>().material = Materials[0];
                PrefabType tempType = new PrefabType();
                Parts.Add(Instantiate(tempObject, tempObject.transform.position, tempObject.transform.rotation, Maze.transform));
                tempType = currentPrefab;

                PartsTypes.Add(tempType);
                noOfParts++;
            }
            else
            {
                Destroy(Parts[toDeleteIndex]);
                Parts.RemoveAt(toDeleteIndex);
                PartsTypes.RemoveAt(toDeleteIndex);
                isDelete = false;
                noOfParts--;
            }

        }
        if (Input.GetKeyDown("z"))
        {
            if (noOfParts > 0)
            {
                Destroy(Parts[noOfParts - 1]);
                Parts.RemoveAt(noOfParts - 1);
                PartsTypes.RemoveAt(noOfParts - 1);
                noOfParts--;
            }
        }
        else if (Input.GetKeyDown("k"))
        {
            currentPrefab = PrefabType.KeyWall;
        }
        else if (Input.GetKeyDown("c"))
        {
            currentPrefab = PrefabType.Coins;
        }
        else if (Input.GetKeyDown("d"))
        {
            currentPrefab = PrefabType.Diamonds;
        }
        else if (Input.GetKeyDown("b"))
        {
            IsStart = true;
            currentPrefab = PrefabType.Start;
        }
        else if (Input.GetKeyDown("e"))
        {
            IsEnd = true;
            currentPrefab = PrefabType.End;
        }
        else if (Input.GetKeyDown("r"))
        {
            currentPrefab = PrefabType.Spike;
        }
        else if (Input.GetKeyDown("x"))
        {
            currentPrefab = PrefabType.CheckPoint;
        }
        else if(Input.GetKeyDown("g"))
        {
            currentPrefab = PrefabType.Guardian;
        }
        else if (Input.GetKeyDown("h"))
        {
            currentPrefab = PrefabType.Hammer;
        }
        else if (Input.GetKeyDown("1"))
        {
            currentPrefab = PrefabType.GoliathWalk;
        }
        else if (Input.GetKeyDown("2"))
        {
            currentPrefab = PrefabType.Goliath;
        }
        else if (Input.GetKeyDown("3"))
        {
            currentPrefab = PrefabType.GoliathAttackSwitch;
        }
        else if (Input.GetKeyDown("`"))
        {
            currentPrefab = PrefabType.Chest;
        }

        
    }

    private void ChangePart()
    {
        switch (currentPrefab)
        {
            case PrefabType.Part01:
                currentPart = ListOfParts[0];
                break;
            case PrefabType.Part02:
                currentPart = ListOfParts[1];
                break;
            case PrefabType.Part03:
                currentPart = ListOfParts[2];
                break;
            case PrefabType.Part04:
                currentPart = ListOfParts[3];
                break;
            case PrefabType.Part05:
                currentPart = ListOfParts[4];
                break;
            case PrefabType.Start:
                currentPart = ListOfParts[5];
                break;
            case PrefabType.End:
                currentPart = ListOfParts[6];
                break;
            case PrefabType.KeyWall:
                currentPart = ListOfParts[7];
                break;
            case PrefabType.Coins:
                currentPart = ListOfParts[8];
                break;
            case PrefabType.KeyPortal:
                break;
            case PrefabType.Spike:
                currentPart = ListOfParts[9];
                break;
            case PrefabType.CheckPoint:
                currentPart = ListOfParts[10];
                break;
            case PrefabType.Guardian:
                currentPart = ListOfParts[11];
                break;
            case PrefabType.Hammer:
                currentPart = ListOfParts[12];
                break;
            case PrefabType.Goliath:
                currentPart = ListOfParts[13];
                break;
            case PrefabType.GoliathWalk:
                currentPart = ListOfParts[14];
                break;
            case PrefabType.GoliathAttackSwitch:
                currentPart = ListOfParts[15];
                break;
            case PrefabType.Chest:
                currentPart = ListOfParts[16];
                break;
            case PrefabType.Diamonds:
                currentPart = ListOfParts[17];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        for (int i = 0; i < index; i++)
        {
            if ((Tiles[i].GetComponent<TileOnClick>().clicked == true || tilePosition.position == Tiles[i].transform.position))
            {
                Tiles[i].GetComponent<TileOnClick>().Reset();
                tilePosition = Tiles[i].transform;
                Destroy(tempObject);
                tempObject = Instantiate(currentPart, tilePosition.position, tilePosition.rotation, Maze.transform);

                tempObject.transform.rotation = angleSet ? orientation : angRef.transform.rotation;
                
                tempObject.GetComponent<Renderer>().material = Materials[1];
            }
        }
        scrolled = false;
    }
    public void Save()  //saving each node to the file  
    {
        nodes = new List<Node>();
        int numberOfCoins = 0;
        int numberOfDiamonds = 0;
        for(int i = 0; i< noOfParts;i++)
        {
            Node tempNode = new Node();
            switch (PartsTypes[i])
            {
                case (PrefabType.Part01):
                    tempNode.Type = PrefabType.Part01;
                    tempNode.transform = Parts[i].transform;
                    break;
                case (PrefabType.Part02):
                    tempNode.Type = PrefabType.Part02;
                    tempNode.transform = Parts[i].transform;
                    break;
                case (PrefabType.Part03):
                    tempNode.Type = PrefabType.Part03;
                    tempNode.transform = Parts[i].transform;
                    break;
                case (PrefabType.Part04):
                    tempNode.Type = PrefabType.Part04;
                    tempNode.transform = Parts[i].transform;
                    break;
                case (PrefabType.Part05):
                    tempNode.Type = PrefabType.Part05;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.Start:
                    tempNode.Type = PrefabType.Start;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.End:
                    tempNode.Type = PrefabType.End;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.KeyWall:
                    tempNode.Type = PrefabType.KeyWall;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.Coins:
                    numberOfCoins++;
                    tempNode.Type = PrefabType.Coins;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.Spike:
                    tempNode.Type = PrefabType.Spike;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.KeyPortal:
                    break;
                case PrefabType.CheckPoint:
                    tempNode.Type = PrefabType.CheckPoint;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.Guardian:
                    tempNode.Type = PrefabType.Guardian;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.Hammer:
                    tempNode.Type = PrefabType.Hammer;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.Goliath:
                    tempNode.Type = PrefabType.Goliath;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.GoliathWalk:
                    tempNode.Type = PrefabType.GoliathWalk;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.GoliathAttackSwitch:
                    tempNode.Type = PrefabType.GoliathAttackSwitch;
                    tempNode.transform = Parts[i].transform;
                    break;
                case PrefabType.Chest:
                    tempNode.Type = PrefabType.Chest;
                    tempNode.transform = Parts[i].transform;
                    
                    var flag = false;
                    TreasureManager.Load();
                    if (TreasureManager.TreasureListAll.Treasures.Count > 0)
                    {
                        foreach (var chest in TreasureManager.TreasureListAll.Treasures)
                        {
                            if (chest.LevelName == SaveManager.levelName)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if(!flag)
                    {
                        TreasureManager.Chest chest = new TreasureManager.Chest();
                        chest.LevelName = SaveManager.levelName;
                        
                        //TODO get the chest index from somewhere
                        chest.ChestIndex = PlayerPrefs.GetInt("NumberOfChests")+1;
                        PlayerPrefs.SetInt("NumberOfChests", PlayerPrefs.GetInt("NumberOfChests")+1);
                        
                        TreasureManager.AddChestToAllTreasures(chest);
                    }                    
                    break;
                case PrefabType.Diamonds:
                    numberOfDiamonds++;
                    tempNode.Type = PrefabType.Diamonds;
                    tempNode.transform = Parts[i].transform;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            nodes.Add(tempNode);
        }
        for(int i = 0; i < noOfParts; i++)
        {
            Destroy(Parts[i]);
        }
        state.Node = new List<SaveavleNode>();
        for(int i = 0; i < noOfParts; i++)
        {
            state.Node.Add(new SaveavleNode());
            state.Node[i].ConvertToSaveable(nodes[i].Type, nodes[i].transform);
        }
        state.LevelSize = SaveManager.levelSize;
        state.MaxTime = 0;
        state.MaxTries = 0;
        state.Coins = numberOfCoins; 
        state.Diamonds = numberOfDiamonds;
        sm.Save(state);
        Load();

        var ls = new LevelStatusSaveState
        {
            LevelName = string.Format("level_{0}_{1}_s", SaveManager.levelName.Split('_')[1], SaveManager.levelName.Split('_')[2]),
            BestTime = 0,
            BestTries = 0,
            IndexOfCoinsCollected = new List<int>(),
            IndexOfDiamondsCollected = new List<int>(),
            IsLocked = false,
            Stars = 0
        };
        ls.Save();
        isSaving = false;

    }
    public int Load()
    {
        state = sm.Load();
        nodes = new List<Node>();
        PartsTypes = new List<PrefabType>();
        Parts = new List<GameObject>();
        for (int i = 0; i < state.Node.Count; i++)
        {
            nodes.Add(state.Node[i].ConvertToNode());
               
        }
        MazeSize = state.LevelSize;
        SaveManager.levelSize = state.LevelSize;
        for (int i = 0; i < state.Node.Count; i++)
        {
            GameObject tempObj = new GameObject();
            switch (nodes[i].Type)
            {

                case (PrefabType.Part01):
                    tempObj = Instantiate(ListOfParts[0], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Part01);
                    break;
                case (PrefabType.Part02):
                    tempObj = Instantiate(ListOfParts[1], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Part02);
                    break;
                case (PrefabType.Part03):
                    tempObj = Instantiate(ListOfParts[2], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Part03);
                    break;
                case (PrefabType.Part04):
                    tempObj = Instantiate(ListOfParts[3], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Part04);
                    break;
                case (PrefabType.Part05):
                    tempObj = Instantiate(ListOfParts[4], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Part05);
                    break;
                case PrefabType.Start:
                    tempObj = Instantiate(ListOfParts[5], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Start);
                    break;
                case PrefabType.End:
                    tempObj = Instantiate(ListOfParts[6], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.End);
                    break;
                case PrefabType.KeyWall:
                    tempObj = Instantiate(ListOfParts[7], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.KeyWall);
                    break;
                case PrefabType.Coins:
                    tempObj = Instantiate(ListOfParts[8], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Coins);
                    break;
                case PrefabType.Spike:
                    tempObj = Instantiate(ListOfParts[9], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Spike);
                    break;
                case PrefabType.KeyPortal:
                    break;
                case PrefabType.CheckPoint:
                    tempObj = Instantiate(ListOfParts[10], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.CheckPoint);
                    break;
                case PrefabType.Guardian:
                    tempObj = Instantiate(ListOfParts[11], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Guardian);
                    break;
                case PrefabType.Hammer:
                    tempObj = Instantiate(ListOfParts[12], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Hammer);
                    break;
                case PrefabType.Goliath:
                    tempObj = Instantiate(ListOfParts[13], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Goliath);
                    break;
                case PrefabType.GoliathWalk:
                    tempObj = Instantiate(ListOfParts[14], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.GoliathWalk);
                    break;
                case PrefabType.GoliathAttackSwitch:
                    tempObj = Instantiate(ListOfParts[15], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.GoliathAttackSwitch);
                    break;
                case PrefabType.Chest:
                    tempObj = Instantiate(ListOfParts[16], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Chest);
                    break;
                case PrefabType.Diamonds:
                    tempObj = Instantiate(ListOfParts[17], nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Diamonds);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Parts.Add(tempObj);
            tempObj.GetComponent<Renderer>().material = Materials[0];

        }
        noOfParts = state.Node.Count;
        mainCamera.orthographicSize = SaveManager.levelSize + 1;
        return 1;   
    }
    public void ChangeMazeSize()
    {
        Body.transform.localScale = new Vector3(MazeSize, MazeSize, MazeSize);
        SpawnTile();
    }
    void SpawnTile()
    {
        startingXpos = (MazeSize - 1) / 2;
        startingYpos = startingXpos;
        heightOffset = MazeSize / 2;
        int n = 0;
        while (n < 3)
        {
            
            for (float i = startingXpos; i >= -startingXpos; i -= 1)
            {
                for (float j = startingYpos; j >= -startingYpos; j -= 1)
                {
                    tilePosition.position = new Vector3(i, heightOffset, j);
                    MazeBody.Add(Instantiate(MazeCube, tilePosition.position, Quaternion.identity, Maze.transform));

                }
            }
            for (float i = startingXpos; i >= -startingXpos; i -= 1)
            {
                for (float j = startingYpos; j >= -startingYpos; j -= 1)
                {
                    tilePosition.position = new Vector3(i, -heightOffset+1, j);
                    MazeBody.Add(Instantiate(MazeCube, tilePosition.position, Quaternion.identity, Maze.transform));

                }
            }

            n += 1;
            if (n == 1)
            {
                Maze.transform.Rotate(90, 0, 0);
                startingYpos -= 1;
            }
            if (n == 2)
            {
                Maze.transform.Rotate(0, 90, 0);
                startingXpos -= 1;
            }
        }

        Maze.transform.Rotate(0, -90, 0);
        Maze.transform.Rotate(-90, 0, 0);
        startingXpos = (MazeSize - 1) / 2;
        startingXpos = (MazeSize - 1) / 2;
        startingYpos = startingXpos;
        heightOffset = MazeSize / 2;
        n = 0;
        while (n < 3)
        {

            for (float i = startingXpos; i >= -startingXpos; i -= 1)
            {
                for (float j = startingYpos; j >= -startingYpos; j -= 1)
                {
                    tilePosition.position = new Vector3(i, heightOffset, j);
                    Tiles.Add(Instantiate(Tile, tilePosition.position, Quaternion.identity, Maze.transform));
                    Tiles[index].AddComponent<TileOnClick>();
                    index++;
                }
            }
            for (float i = startingXpos; i >= -startingXpos; i -= 1)
            {
                for (float j = startingYpos; j >= -startingYpos; j -= 1)     
                {
                    tilePosition.position = new Vector3(i, -heightOffset, j);  
                    Tiles.Add(Instantiate(Tile, tilePosition.position, Quaternion.identity, Maze.transform));
                    Tiles[index].AddComponent<TileOnClick>();
                    index++;
                }
            }

            n += 1;
            if (n == 1)
            {
                Maze.transform.Rotate(90, 0, 0);
            }
            if (n == 2)
            {
                Maze.transform.Rotate(0, 90, 0);
            }
        }

        Maze.transform.Rotate(0, -90, 0);
        Maze.transform.Rotate(-90, 0, 0);
    }

}

