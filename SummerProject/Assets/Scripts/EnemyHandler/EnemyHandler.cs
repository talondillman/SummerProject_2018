using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MovingObjects {

    public static GameObject thisMovingEnemy;

    /// <summary>
    /// Make this enemy static and accesible everywhere
    /// then calls base.Start()
    /// </summary>
    protected override void Start()
    {
        if (thisMovingEnemy != null) { Destroy(this.gameObject); return;}
        thisMovingEnemy = this.gameObject;
        base.Start();
    }

    /// <summary>
    /// calls CheckGround()
    /// then moves the player if still grounded
    /// </summary>
    protected void FixedUpdate()
    {
        CheckGround();

        Vector3 velocity = myRigidBody.velocity;
        velocity.x = myTransform.forward.x * speed;
        myRigidBody.velocity = velocity;
        
    }

}
