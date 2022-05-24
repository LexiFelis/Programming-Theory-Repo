using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Knife enemy:
// Inhereted: Patrols, hurts player
// polymorphism: rotates to face direction of movement, directional collision detection

public class Knife : Enemy
{
    public override bool canDie
    {
        get { return m_canDie; }
        protected set { m_canDie = value; }
    }

    public override bool directionInverse
    {
        get { return m_directionInverse; }
        protected set { m_directionInverse = value; }
    }

    public override float contactThreshold
    {
        get { return m_contactThreshold; }
        protected set { m_contactThreshold = value; }
    }

    public override Vector3 validDirection
    {
        get { return m_validDirection; }
        protected set { m_validDirection = value; }
    }

    protected override void Start()
    {
        canDie = true;
        validDirection = Vector3.up; // side of enemy we are checking (top)
        contactThreshold = 30; // Acceptable difference in degrees
        directionInverse = false;
        doesRotate = true;
        patrolRotate = new Vector3(180, 0, 0);
        base.Start();
    }
}
