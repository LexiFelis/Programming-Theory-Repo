using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fork Enemy:
//Inherited: Patrols from point A to B, hurts player on contact
//Polymorphism: Cannot be destroyed
public class Fork : Enemy
{

    protected override void Start()
    {
        doesRotate = false;        
        canDie = false;
        base.Start();
    }
}
