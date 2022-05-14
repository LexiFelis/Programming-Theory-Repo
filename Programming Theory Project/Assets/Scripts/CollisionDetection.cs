using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles collision detection for the player avatar.
public class CollisionDetection : MonoBehaviour
{
    private PlayerController playerController;
    //public GameObject mainController;
    public MainController mainController;

    private void Awake()
    {
         playerController = GetComponent<PlayerController>();
    }

    // Colliders are here, see below for triggers

    private void OnCollisionEnter(Collision other)
    {
        // Detects collision with hazards/enemies and initial collision with the ground
        if (other.gameObject.tag == "hazard")
        {
            if (!playerController.hitCooldown)
            {
                playerController.HazardCollide();
            }
        }
        if (other.gameObject.tag == "ground")
        {
            playerController.grounded = true;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        // Detects continued collision with ground objects
        if (other.gameObject.tag == "ground")
        {
            playerController.grounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // Detects player leaving ground object, to stop them being able to jump after rolling off a surface.
        if (other.gameObject.tag == "ground")
        {
            playerController.grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detects hitting the goal box.
        if (other.gameObject.name == "Goal")
        {
            mainController.GameWin();
        }
    }
}
