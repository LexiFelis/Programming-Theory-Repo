using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fork Enemy:
//Inherited: Patrols from point A to B, hurts player on contact
//Polymorphism: Cannot be destroyed, has configurable delay to patrol to allow differing patterns of "stabbing"
public class Fork : Enemy // INHERITENCE
{
    [SerializeField] private float delayBy;

    public override bool canDie
    {
        get { return m_canDie; }
        protected set { m_canDie = value; }
    }// ENCAPSULATION
    public override float contactThreshold
    {
        get { return m_contactThreshold; }
        protected set { m_contactThreshold = value; }
    }// ENCAPSULATION

    public override Vector3 validDirection
    {
        get { return m_validDirection; }
        protected set { m_validDirection = value; }
    }// ENCAPSULATION
    public override bool directionInverse
    {
        get { return m_directionInverse; }
        protected set { m_directionInverse = value; }
    }// ENCAPSULATION

    protected override void Start()
    {
        InitialiseFork();// ABSTRACTION
        base.Start();
    }

    private void InitialiseFork() // POLYMORPHISM
    {
        enemyObj = gameObject;
        canDie = false;
        validDirection = Vector3.down; // side of enemy we are checking (top)
        contactThreshold = 30; // Acceptable difference in degrees
        directionInverse = true;
        hasDelay = true;
        delayTime = delayBy;
        doesRotate = false;
    }
}
