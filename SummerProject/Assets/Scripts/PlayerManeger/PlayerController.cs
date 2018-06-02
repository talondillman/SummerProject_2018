using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static CharacterController CharacterController;
    private static PlayerController Instance;
    private GameObject TargetLookAt;

    [SerializeField] float velocityX = 0f;
    [SerializeField] float velocityY = 0f;
    [SerializeField] float X_Smooth = 0.05f;
    [SerializeField] float Y_Smooth = 0.01f;
    [SerializeField] float Change_X = 5f;
    [SerializeField] float Change_Y = 2f;


    //Used for path camera
    private Vector3 position = Vector3.zero;

    void Awake()
    {
        if(Instance != null)
        {
            return;
        }
        CharacterController = GetComponent("CharacterController") as CharacterController;
        Instance = this;

        TargetLookAt = GameObject.Find("targetLookAt");

        DontDestroyOnLoad(Instance);
        CameraController.UsingExistingOrCreateNewMainCamera();//check camera
    }
	
	void Update ()
    {
        

        
        //If the camera doesn't exist don't do anything
        if (Camera.main == null)
        {
            return;
        }

        //If player isn't moving look at the player
        if (CharacterController.velocity == Vector3.zero)
        {
            //ChangeTargetLookAt(0, 0);
        }else if(CharacterController.velocity.x > 0) //if moving right look right.
        {
            ChangeTargetLookAt(Change_X, Change_Y);
        }else if(CharacterController.velocity.x < 0) //if moving left look left
        {
            ChangeTargetLookAt(-Change_X, Change_Y);
        }

       
        GetLocomotionInput();
        HandleActionInput();

        PlayerMotor.Instance.UpdateMotor();
       
	}
    /// <summary>
    /// For Camera smoothing
    /// </summary>
    /// <param name="x"> x distance to move targetLookAt</param>
    /// <param name="y"> y distance to move targetLookAt</param>
    private void ChangeTargetLookAt(float x, float y)
    {
        Vector3 playerPosition = CharacterController.transform.position;

        float posX = Mathf.SmoothDamp(position.x, playerPosition.x + x, ref velocityX, X_Smooth);
        float posY = Mathf.SmoothDamp(position.y, playerPosition.y + y, ref velocityY, Y_Smooth);

        position = new Vector3(playerPosition.x + x, playerPosition.y + y, playerPosition.z);
        //position = new Vector3(posX, posY, playerPosition.z);
        TargetLookAt.transform.position = position;
    }

    void GetLocomotionInput()
    {
        float deadZone = 0.1f; //Used for controllers

        //zero movement to get new speed every frame
        PlayerMotor.Instance.VeritcalVelocity = PlayerMotor.Instance.MoveVector.y; //saves Vertical Velocity
        //Debug.Log("Saving: " + PlayerMotor.Instance.VeritcalVelocity);
        PlayerMotor.Instance.MoveVector = Vector3.zero;

        if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
        {
            PlayerMotor.Instance.MoveVector += new Vector3( Input.GetAxis("Horizontal"), 0, 0);
        }
    }

    private void HandleActionInput()
    {
        if (Input.GetButton("Jump")) {
            Jump();
        }
    }

    //Calls Jump
    private void Jump()
    {
        PlayerMotor.Instance.Jump();
    }

}
