using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : Pickup // INHERITENCE
{
    protected override void Start()
    {
        pickUp = gameObject;
        base.Start();
    }


    protected override void PowerUp() // POLYMORPHISM
    {
        playerController = player.GetComponent<PlayerController>();

        if (playerController.playerLivesRemain < playerController.playerLifeMax)
        {
            playerController.playerLivesRemain++;
            gameObject.SetActive(false);
        }
    }
}
