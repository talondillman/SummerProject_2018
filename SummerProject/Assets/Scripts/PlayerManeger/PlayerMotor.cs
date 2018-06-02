using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public static PlayerMotor Instance;
    public float moveSpeed = 10f;
    public float Gravity = 21f;
    public float TerminalVelocity = 20f;
    public float JumpSpeed = 6f;

    public Vector3 MoveVector { get; set; }
    public float VeritcalVelocity { get; set; }

    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void UpdateMotor () {

        //SnapAllignCharacterWithCamera();
        ProccessMotion();
	}

    void ProccessMotion()
    {
       
        MoveVector = transform.TransformDirection(MoveVector);

        if(MoveVector.magnitude > 1)
        {
            MoveVector = Vector3.Normalize(MoveVector);
        }
        MoveVector *= moveSpeed;

        MoveVector = new Vector3(MoveVector.x, VeritcalVelocity, MoveVector.z);
        ApplyGravity();


        PlayerController.CharacterController.Move(MoveVector * Time.deltaTime);

    }

    //Make things fall, the player mostly
    void ApplyGravity()
    {
        //But not to fast
        if(MoveVector.y > -TerminalVelocity)
        {
            MoveVector = new Vector3(MoveVector.x, MoveVector.y - Gravity * Time.deltaTime, MoveVector.z);
        }

        if (PlayerController.CharacterController.isGrounded && MoveVector.y < -1)
        {
            MoveVector = new Vector3(MoveVector.x, -1, MoveVector.z);
        }
    }

    public void Jump()
    {
        if (PlayerController.CharacterController.isGrounded)
        {
            VeritcalVelocity = JumpSpeed;
        }
    }

    //Used to keep player alligned with camera
    //Obviously unescesary for 2.5D
    void SnapAllignCharacterWithCamera()
    {
        if(MoveVector.x != 0 || MoveVector.z != 0)
        {
            //Euler is get the angles.
            //transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
