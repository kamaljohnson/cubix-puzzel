using System;
using System.Collections.Generic;
using UnityEngine;
/*
 * 1. script to direct the camara to move (HINT make the camara move over to the corners of am imaginary cube over the main maze cube)
 * 2. move the camera to the next position by using the direction of movement of the player and the current position of the camera  
 */

public class playerController : MonoBehaviour
{

    public bool game_level_loaded = false;
    public GameObject player;
    //all the variables used to controll the external scripts 
    public bool playerCameraIsRotating = false;
    float MazeSize = 5; //store the size of the maze (the dimention of the maze)
    /* TODO : make the script get the size of the maze from code
     */
    private float MazeOffset;

    //components for the player rotaration (the ground trigger items)
    //all the colliders are set to isTrigger
    public GameObject Right;
    public GameObject Left;
    public GameObject Forward;
    public GameObject Back;
    public GameObject Down;

    RayCastScript rightRay;
    RayCastScript leftRay;
    RayCastScript forwardRay;
    RayCastScript backRay;
    RayCastScript downRay;

    SwipeControl swipeInput;

    public GameObject maze_body;
    public GameObject MazeBody;
    MazeBodyRotation mazeRotation;

    bool canMoveRight = false;
    bool canMoveLeft = false;
    bool canMoveForward = false;
    bool canMoveBack = false;

    bool isMoving = false;
    bool inJunction = true;

    float stepOffSet = 1f;   //the distance between each step (start and the end)
    float DownStep;

    private float playerMeshSize = 0.25f;

    bool trigFlag;
    bool rotateFlag;

    enum Direction  //direction of the player movement
    {
        Null,
        Right,
        Left,
        Forward,
        Back
    };
    Direction tempDirection;
    //all the variables to controll the player 
    private float playerSpeed = 1.0f;  //0.05 correct value //the speed of the player also manipulates the animation speed of the player 
    private bool atEdge;    //to check if the player reached the edge of the maze(to trigger the camera movement and the player rotation)
    private Direction movementDireciton;

    Vector3 direction;

    Vector3 localRight;
    Vector3 localLeft;
    Vector3 localForward;
    Vector3 localBack;
    Vector3 localDown;

    Vector3 RightRotation;
    Vector3 LeftRotation;
    Vector3 ForwardRotation;
    Vector3 BackRotation;

    Vector3 destination = new Vector3();
    bool destinationFlag = false;

    AnimationScript anim;
    public GameObject cubeMesh;
    bool animEdgeFlag; //for the continuation of the animation after the maze rotation 
    float animationSpeed = 2.0f;


    SaveManager sm = new SaveManager();
    SaveState state = new SaveState();
    List<Node> nodes = new List<Node>();
    List<GameObject> Parts = new List<GameObject>();
    List<PrefabType> PartsTypes = new List<PrefabType>();
    public GameObject Maze;
    public GameObject Part01;
    public GameObject Part02;
    public GameObject Part03;
    public GameObject Part04;
    public GameObject Part05;
    public Material onMaze;
    public Camera mainCamera;

    int noOfParts = 0;

    private void Start () {

        Screen.orientation = ScreenOrientation.Portrait;
        swipeInput = GetComponent<SwipeControl>();

        anim = cubeMesh.GetComponent<AnimationScript>();
        animEdgeFlag = false;
        isMoving = false;
        
        rightRay = Right.GetComponent<RayCastScript>();
        leftRay = Left.GetComponent<RayCastScript>();
        forwardRay = Forward.GetComponent<RayCastScript>();
        backRay = Back.GetComponent<RayCastScript>();
        downRay = Down.GetComponent<RayCastScript>();

        RightRotation = Vector3.back;
        LeftRotation = Vector3.forward;
        ForwardRotation = Vector3.right;
        BackRotation = Vector3.left;

        trigFlag = false;
        rotateFlag = false;
        atEdge = false;
        destinationFlag = true;
        //destination = transform.localPosition;

        mazeRotation = MazeBody.GetComponent<MazeBodyRotation>();

        Load();

    }
	
	private void FixedUpdate ()
    {
        if (GameManager.IsLevelCompleted)
        {
            game_level_loaded = false;
            Load();
            GameManager.IsLevelCompleted = false;
            GameManager.IsStart = true;
        }
        if (GameManager.IsStart)
        {
            transform.localPosition = GameManager.StartPosition;
            GameManager.IsStart = false;
            GameManager.IsPlaying = true;
            destination = transform.localPosition;
        }
        if (GameManager.IsPlaying)
        {
            WallCollisionCheck();
            EdgeDetection();
            JunctionDetection();
            localForward = transform.parent.InverseTransformDirection(transform.forward);
            localRight = transform.parent.InverseTransformDirection(transform.right);
            localLeft = localRight * -1;
            localBack = localForward * -1;
            localDown = transform.parent.InverseTransformDirection(transform.up) * -1;


            if (animEdgeFlag && mazeRotation.rotate)
            {
                anim.animationStop = true;
            }
            else if (animEdgeFlag && !mazeRotation.rotate)
            {
                switch (movementDireciton)
                {
                    case Direction.Right:
                        anim.right = true;
                        break;
                    case Direction.Left:
                        anim.left = true;
                        break;
                    case Direction.Forward:
                        anim.forward = true;
                        break;
                    case Direction.Back:
                        anim.back = true;
                        break;
                }

                animEdgeFlag = false;
            }

            if (inJunction)
            {
                anim.animationStop = true;

            }

            if (!atEdge)
            {
                Move();
            }

            if (atEdge)
            {
                animEdgeFlag = true;
                ChangePlane();
            }
        }
    }
    void JunctionDetection()
    {
        if ((movementDireciton == Direction.Right || movementDireciton == Direction.Left) && (canMoveForward || canMoveBack))
        {
            inJunction = true;
        }
        else if ((movementDireciton == Direction.Forward || movementDireciton == Direction.Back) && (canMoveRight || canMoveLeft))
        {
            inJunction = true;
        }
        else if(movementDireciton == Direction.Right && !canMoveRight)
        {
            inJunction = true;
        }
        else if (movementDireciton == Direction.Left && !canMoveLeft)
        {
            inJunction = true;
        }
        else if (movementDireciton == Direction.Forward && !canMoveForward)
        {
            inJunction = true;
        }
        else if (movementDireciton == Direction.Back && !canMoveBack)
        {
            inJunction = true;
        }
        else
        {
            inJunction = false;
        }
    }
    void Move() //controls the movement of the player 
    {
        if (((Input.GetAxis("Horizontal") > 0  || swipeInput.Right)&& !mazeRotation.rotate)|| tempDirection == Direction.Right)
        {
            if (canMoveRight)
            {
                tempDirection = Direction.Right;
                if (destinationFlag)
                {
                    swipeInput.Right = false;
                    isMoving = true;
                    destinationFlag = false;
                    movementDireciton = Direction.Right;
                    destination = transform.localPosition + localRight * stepOffSet;
                    anim.right = true; 
                }
            }
        }
        if(((Input.GetAxis("Horizontal") < 0 || swipeInput.Left) && !mazeRotation.rotate) || tempDirection == Direction.Left)
        {
            if (canMoveLeft)
            {
                tempDirection = Direction.Left;
                if (destinationFlag)
                {
                    swipeInput.Left = false;
                    isMoving = true;
                    destinationFlag = false;
                    movementDireciton = Direction.Left;
                    destination = transform.localPosition + localLeft * stepOffSet;
                    anim.left = true;
                }
            }
        }
        if(((Input.GetAxis("Vertical") > 0 || swipeInput.Forward) && !mazeRotation.rotate) || tempDirection == Direction.Forward)
        {
            if (canMoveForward)
            {
                tempDirection = Direction.Forward;
                if (destinationFlag)
                {
                    swipeInput.Forward = false;
                    isMoving = true;
                    destinationFlag = false;
                    movementDireciton = Direction.Forward;
                    destination = transform.localPosition + localForward * stepOffSet;
                    anim.forward = true;
                }
            }
        }
        if(((Input.GetAxis("Vertical") < 0 || swipeInput.Back)&& !mazeRotation.rotate) || tempDirection == Direction.Back)
        {
            if (canMoveBack)
            {
                tempDirection = Direction.Back;
                if (destinationFlag)
                {
                    swipeInput.Back = false;
                    isMoving = true;
                    destinationFlag = false;
                    movementDireciton = Direction.Back;
                    destination = transform.localPosition + localBack * stepOffSet;
                    anim.back = true;
                }
            }
        }
        
        if (transform.localPosition == destination)
        {
            if (GameManager.EndPosition == transform.localPosition)
            {
                GameManager.IsLevelCompleted = true;
                GameManager.IsPlaying = false;
                transform.localPosition = destination;
            }
            destinationFlag = true;
            if (inJunction)
            {
                tempDirection = Direction.Null;
                movementDireciton = Direction.Null;
                isMoving = false;
            }
        }
        else if(!mazeRotation.rotate)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, Time.deltaTime * playerSpeed * animationSpeed);
        }
    }
    void WallCollisionCheck()
    {
        if(rightRay.hittingWall)
        {
            canMoveRight = false;
        }
        else
        {
            canMoveRight = true;
        }

        if (leftRay.hittingWall)
        {
            canMoveLeft = false;
        }
        else
        {
            canMoveLeft = true;
        }

        if (forwardRay.hittingWall)
        {
            canMoveForward = false;
        }
        else
        {
            canMoveForward = true;
        }

        if (backRay.hittingWall)
        {
            canMoveBack = false;
        }
        else
        {
            canMoveBack = true;
        }

    }
    void EdgeDetection()
    {
        /*float overShoot = 0.1f;
        if(transform.localPosition.x > MazeOffset + overShoot)
        {
            atEdge = true;
            transform.localPosition = new Vector3(MazeOffset, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.y > MazeOffset + overShoot)
        {
            atEdge = true;
            transform.localPosition = new Vector3(transform.localPosition.x, MazeOffset, transform.localPosition.z);
        }
        else if (transform.localPosition.z > MazeOffset + overShoot)
        {
            atEdge = true;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, MazeOffset);
        }
        else if (transform.localPosition.x < -(MazeOffset + overShoot))
        {
            atEdge = true;
            transform.localPosition = new Vector3(-MazeOffset, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.y < -(MazeOffset + overShoot))
        {
            atEdge = true;
            transform.localPosition = new Vector3(transform.localPosition.x, -MazeOffset, transform.localPosition.z);
        }
        else if (transform.localPosition.z < -(MazeOffset + overShoot))
        {
            atEdge = true;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -MazeOffset);
        }*/

        if(!downRay.hittingWall)
        {
            atEdge = true;
        }
        
    }
    void ChangePlane()
    {
        if (!rotateFlag)
        {
            destination = transform.localPosition + localDown * DownStep;
            PlayerRotate();

            RotateMaze();

            rotateFlag = true;
        }
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, Time.deltaTime * playerSpeed * animationSpeed);
        if (transform.localPosition == destination)
        {
            transform.localPosition = destination;
            rotateFlag = false;
            atEdge = false;
            destinationFlag = true;
            if (inJunction)
            {
                tempDirection = Direction.Null;
                movementDireciton = Direction.Null;
                isMoving = false;
            }

        }
        if (transform.localPosition.x > MazeOffset)
        {
            transform.localPosition = new Vector3(MazeOffset, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.y > MazeOffset)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, MazeOffset, transform.localPosition.z);
        }
        else if (transform.localPosition.z > MazeOffset)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, MazeOffset);
        }
        else if (transform.localPosition.x < -MazeOffset)
        {
            transform.localPosition = new Vector3(-MazeOffset, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.y < -MazeOffset)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -MazeOffset, transform.localPosition.z);
        }
        else if (transform.localPosition.z < -MazeOffset)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -MazeOffset);
        }
    }
    void PlayerRotate()
    {

        for (int i = 0; i < 90; i++)
        {
           
            if (movementDireciton == Direction.Right)
            {
                transform.Rotate(RightRotation);
            }
            else if (movementDireciton == Direction.Left)
            {
                transform.Rotate(LeftRotation);
            }
            else if (movementDireciton == Direction.Forward)
            {
                transform.Rotate(ForwardRotation);
            }
            else if (movementDireciton == Direction.Back)
            {
                transform.Rotate(BackRotation);
            }
        }

    }

    void RotateMaze() 
    {
        mazeRotation.rotate = true;
        
        if (movementDireciton == Direction.Right)
        {
            mazeRotation.rotateDirection = localForward * 2;
        }
        else if (movementDireciton == Direction.Left)
        {
            mazeRotation.rotateDirection = localBack * 2;
        }
        else if (movementDireciton == Direction.Forward)
        {
            mazeRotation.rotateDirection = localLeft * 2;
        }
        else if (movementDireciton == Direction.Back)
        {
            mazeRotation.rotateDirection = localRight * 2;
        }
    }
    public int Load()
    {
        SaveManager.levelName = GameManager.CurrentLevel;
        state = sm.Load();
        nodes = new List<Node>();
        PartsTypes = new List<PrefabType>();
        Parts = new List<GameObject>();
        for (int i = 0; i < state.node.Count; i++)
        {
            nodes.Add(state.node[i].ConvertToNode());
        }
        MazeSize = state.levelSize;
        MazeOffset = MazeSize / 2;
        DownStep = MazeOffset / MazeSize;
        SaveManager.levelSize = state.levelSize;
        var start_end_flag = false;
        for (var i = 0; i < state.node.Count; i++)
        {
            start_end_flag = false;
            GameObject tempObj = new GameObject();
            switch (nodes[i].Type)
            {

                case (PrefabType.Part01):
                    tempObj = Instantiate(Part01, nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Part01);
                    break;
                case (PrefabType.Part02):
                    tempObj = Instantiate(Part02, nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Part02);
                    break;
                case (PrefabType.Part03):
                    tempObj = Instantiate(Part03, nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Part03);
                    break;
                case (PrefabType.Part04):
                    tempObj = Instantiate(Part04, nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Part04);
                    break;
                case (PrefabType.Part05):
                    tempObj = Instantiate(Part05, nodes[i].transform.position, nodes[i].transform.rotation, Maze.transform);
                    PartsTypes.Add(PrefabType.Part05);
                    break;
                case PrefabType.Start:
                    start_end_flag = true;
                    GameManager.StartPosition = nodes[i].transform.position;
                    break;
                case PrefabType.End:
                    start_end_flag = true;
                    GameManager.EndPosition = nodes[i].transform.position;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (start_end_flag) continue;
            tempObj.GetComponent<Renderer>().material = onMaze;
            Parts.Add(tempObj);
        }
        noOfParts = state.node.Count;
        maze_body.transform.localScale = new Vector3(SaveManager.levelSize, SaveManager.levelSize, SaveManager.levelSize);
        mainCamera.orthographicSize = SaveManager.levelSize + 7;
        /*destination = transform.localPosition;*/
        game_level_loaded = true;
        return 1;
    }
}
