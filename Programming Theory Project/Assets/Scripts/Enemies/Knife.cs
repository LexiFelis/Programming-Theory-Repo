using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Knife enemy:
// Inhereted: Patrols, hurts player
// polymorphism: rotates to face direction of movement, directional collision detection

public class Knife : Enemy // INHERITENCE
{
    public override bool canDie
    {
        get { return m_canDie; }
        protected set { m_canDie = value; }
    } // ENCAPSULATION

    public override bool directionInverse
    {
        get { return m_directionInverse; }
        protected set { m_directionInverse = value; }
    } // INHERITENCE // POLYMORPHISM

    public override float contactThreshold
    {
        get { return m_contactThreshold; }
        protected set { m_contactThreshold = value; }
    } // INHERITENCE // POLYMORPHISM

    public override Vector3 validDirection
    {
        get { return m_validDirection; }
        protected set { m_validDirection = value; }
    } // INHERITENCE // POLYMORPHISM

    protected override void Start() // INHERITENCE
    {
        InitialiseKnife(); // ABSTRACTION
        base.Start(); // INHERITENCE
    }

    private void InitialiseKnife() // POLYMORPHISM
    {
        enemyObj = gameObject;
        canDie = true;
        validDirection = Vector3.up; // side of enemy we are checking (top)
        contactThreshold = 30; // Acceptable difference in degrees
        directionInverse = false;
        patrolRotate = new Vector3(180, 0, 0);
        doesRotate = true;
    }
}
