using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fork Enemy:
//Inherited: Patrols from point A to B, hurts player on contact
//Polymorphism: Cannot be destroyed, has configurable delay to patrol to allow differing patterns of "stabbing"
public class Fork : Enemy
{
    [SerializeField] private float delayBy;

    protected override void Start()
    {
        hasDelay = true;
        delayTime = delayBy;
        doesRotate = false;
        canDie = false;
        base.Start();
    }
    }
