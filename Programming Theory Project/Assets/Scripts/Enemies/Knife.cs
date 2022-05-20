using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Knife enemy:
// Inhereted: Patrols, hurts player
// polymorphism: rotates to face direction of movement, directional collision detection

public class Knife : Enemy
{
    protected override void Start()
    {
        validDirection = Vector3.down; // side of Player we are checking (bottom)
        contactThreshold = 30; // Acceptable difference in degrees
        doesRotate = true;
        patrolRotate = new Vector3(180, 0, 0);
        canDie = true;
        base.Start();
    }
}
