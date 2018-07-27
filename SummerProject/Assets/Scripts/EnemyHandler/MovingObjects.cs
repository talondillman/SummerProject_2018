using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Abstract class to help all objects AI movement with pathing
/// </summary>
public abstract class MovingObjects : MonoBehaviour {

    public float speed;

    protected BoxCollider boxCollider;
    protected Rigidbody myRigidBody;
    protected Transform myTransform;
    protected float width;

    

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        myRigidBody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
        width = boxCollider.bounds.extents.x;
        
    }
    /// <summary>
    /// Checks the players position against the ground and flips the direction if it is going to walk over the edge of a platform
    /// </summary>
    protected void CheckGround()
    {
        Vector3 lineCastPosition = myTransform.position + myTransform.right * width;
        //Debug.DrawLine(lineCastPosition, lineCastPosition + Vector3.down);
        bool isGrounded = Physics.Linecast(lineCastPosition, lineCastPosition + Vector3.down);
        //if there is no ground turn around 
        if (!isGrounded) {
            /*
             * Commentted out b/c it's irrelevant right now.
             * Animation will change this.
             */
            Vector3 currentRotation = myTransform.eulerAngles;
            currentRotation.y += 180;
            myTransform.eulerAngles = currentRotation;
        }

       
    }
}
