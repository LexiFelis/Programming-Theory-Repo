using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steak : Pickup
{
    [SerializeField] float invincibilityTime;

    protected override void Start()
    {
        pickUp = gameObject;
        base.Start();
    }

    protected override void PowerUp()
    {
        playerController = player.GetComponent<PlayerController>();

        if (!playerController.hitCooldown)
        {
            playerController.InvincibilityTrigger(invincibilityTime);
            gameObject.SetActive(false);
        }
    }
}
