using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MovingObjects {

    protected override void Start()
    {
        base.Start();
    }

    protected void FixedUpdate()
    {
        CheckGround();

        Vector3 velocity = myRigidBody.velocity;
        velocity.x = myTransform.right.x * speed;
        myRigidBody.velocity = velocity;
        
    }

}
