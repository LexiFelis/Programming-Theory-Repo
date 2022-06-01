using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steak : Pickup // INHERITENCE
{
    [SerializeField] float invincibilityTime;

    protected override void Start()
    {
        pickUp = gameObject;
        base.Start();
    }

    protected override void PowerUp() // POLYMORPHISM
    {
        playerController = player.GetComponent<PlayerController>();

        if (!playerController.hitCooldown)
        {
            playerController.InvincibilityTrigger(invincibilityTime);
            gameObject.SetActive(false);
        }
    }
}
