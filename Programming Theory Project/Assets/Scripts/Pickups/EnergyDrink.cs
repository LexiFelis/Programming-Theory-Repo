using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : Pickup
{


    private void Start()
    {
        StartCoroutine(MoveTimer());
    }

    private void Update()
    {
        PickupRotation();
    }

    protected override void PowerUp()
    {
        playerController = player.GetComponent<PlayerController>();

        if(playerController.playerLivesRemain <= 3)
        {
            playerController.playerLivesRemain++;
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }    
    }
}
