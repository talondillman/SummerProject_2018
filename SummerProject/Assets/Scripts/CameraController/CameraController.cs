using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public static CameraController Instance;

    public Transform TargetLookAt;
    //public Transform PathLookAt;

    [SerializeField] float Distance = 5f; // How far Away the camera should be.
    [SerializeField] float DistanceMin = 1f; // How close the camera can be
    [SerializeField] float DistanceMax = 15f; //How far away the camera can be
    [SerializeField] float followDistance = 1f; // How far behind the character the camera will be
    [SerializeField] float followHeight = 1f; // How high up the camera is from the player
    [SerializeField] float tilt = 15f; //degree to tilt the camera down
   
    [SerializeField] float MouseWheelSensitivity = 5f; 
    
    //transition to new camera location in time (seconds)
    public float DistanceSmooth = 0.05f;
    public float X_Smooth = 0.05f;
    public float Y_Smooth = 0.1f;
   
    private float startDistance = 0f;
    private float desiredDistance = 0f;
   
    //store the point along "smoothing curve"
    private float velocityDistance = 0f;
    private Vector3 position = Vector3.zero;
    private Vector3 desiredPosition = Vector3.zero;
    private float velocityX = 0f;
    private float velocityY = 0f;
    
    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Distance = Mathf.Clamp(Distance, DistanceMax, DistanceMin);
        startDistance = Distance;
        Reset();
    }

    private void LateUpdate()
    {
        //check for place to look at
        if (TargetLookAt == null)
        {
            return;
        }
        
        //HandelPlayerInput();
        UpdatePosition();
    }

    //Change camera with mouse
    //Scroll is important 
    public void HandelPlayerInput()
    {
        //place not to work
        float deadZone = 0.01f;

        if(Input.GetAxis("Mouse ScrollWheel") < -deadZone || Input.GetAxis("Mouse ScrollWheel") > deadZone)
        {
            Debug.Log("Scrolling Mouse Wheel");
            desiredDistance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity, DistanceMin, DistanceMax);
        }
    }
    
    /// <summary>
    /// Updates the position of the camera gradually not an instant jump
    /// </summary>
    void UpdatePosition()
    {
        //Follow on player
        float posX = Mathf.SmoothDamp(position.x, TargetLookAt.position.x - followDistance, ref velocityX, X_Smooth);
        float posY = Mathf.SmoothDamp(position.y, TargetLookAt.position.y + followHeight, ref velocityY, Y_Smooth);

        position = new Vector3(posX, posY, -Distance);
        //Debug.Log("PosX: " + posX + " PosY: " + posY + " Desired Distance: " + -Distance);
        //Debug.Log("Camera Position: " + position.ToString());
        transform.position = position;
        transform.eulerAngles = new Vector3(tilt, 0, 0);
        //transform.LookAt(TargetLookAt);
    }

    //Make sure stuff is within bounds
    public void Reset()
    {
        Distance = startDistance;
        desiredDistance = Distance;
    }
    //Make sure the camera exists
    public static void UsingExistingOrCreateNewMainCamera()
    {
        GameObject tempCamera;
        GameObject targetLookAt;
        //GameObject pathLookAt;
        CameraController myCamera;

        //Look for a camera, if it exits assign it, otherwise make it
        if(Camera.main != null)
        {
            tempCamera = Camera.main.gameObject;
        } else
        {
            tempCamera = new GameObject("Main Camera");
            tempCamera.AddComponent<Camera>();
            tempCamera.tag = "MainCamera";
        }

        tempCamera.AddComponent<CameraController>();
        myCamera = tempCamera.GetComponent<CameraController>() as CameraController;

        //Find where to look at
        targetLookAt = GameObject.Find("targetLookAt") as GameObject;
  
        myCamera.TargetLookAt = targetLookAt.transform; //assigning global variable
        //myCamera.PathLookAt = pathLookAt.transform;
    }

}
