using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    /// <summary>
    /// The Directions the Player can move
    /// </summary>
    public enum Direction {
        Stationary, Forward, Backward
    }

    public static PlayerAnimator Instance;

    public Direction MoveDirection { get; set; }

	// Use this for initialization
	void Awake () {
		
        if(Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;
	}


    void Update () {
		
	}

    /// <summary>
    /// Determines wether of not the player is moving and what direction that is.
    /// </summary>
    public void DetermineCurrentMoveDirection()
    {
        bool forward = false;
        bool backward = false;

        if (PlayerMotor.Instance.MoveVector.x > 0) {
            forward = true;
        }
        if (PlayerMotor.Instance.MoveVector.x < 0) {
            backward = true;
        }

        if (forward) {
            MoveDirection = Direction.Forward;
        }
        else if (backward) {
            MoveDirection = Direction.Backward;
        }
        else {
            MoveDirection = Direction.Stationary;
        }
    }
}
