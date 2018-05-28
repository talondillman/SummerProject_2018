using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static CharacterController CharacterController;
    private static PlayerController Instance;
    private GameObject PathLookAt;

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

        PathLookAt = GameObject.Find("pathLookAt");

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
            ChangePathLookAt(0, 0);
        }else if(CharacterController.velocity.x > 0) //if moving right look right.
        {
            ChangePathLookAt(Change_X, Change_Y);
        }else if(CharacterController.velocity.x < 0) //if moving left look left
        {
            ChangePathLookAt(-Change_X, Change_Y);
        }

        HandleActionInput();
        GetLocomotionInput();
        PlayerMotor.Instance.UpdateMotor();
       
	}

    private void ChangePathLookAt(float x, float y)
    {
        Vector3 playerPosition = CharacterController.transform.position;

        float posX = Mathf.SmoothDamp(position.x, playerPosition.x + x, ref velocityX, X_Smooth);
        float posY = Mathf.SmoothDamp(position.y, playerPosition.y + y, ref velocityY, Y_Smooth);

        position = new Vector3(posX, posY, playerPosition.z);
        PathLookAt.transform.position = position;
    }

    private void HandleActionInput()
    {
        if (Input.GetButton("Jump"))
        {
            
            Jump();
        }
    }

    //Calls Jump
    private void Jump()
    {
        PlayerMotor.Instance.Jump();
    }
    
    void GetLocomotionInput()
    {
        float deadZone = 0.1f; //Used for controllers
        //zero movement to get new every frame
        PlayerMotor.Instance.VeritcalVelocity = PlayerMotor.Instance.MoveVector.y; //saves Vertical Velocity so that you can keep it.
        PlayerMotor.Instance.MoveVector = Vector3.zero;

        if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
        {
            PlayerMotor.Instance.MoveVector += new Vector3( Input.GetAxis("Horizontal"), 0, 0);
        }
    }

    
}
