using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles non-enemy collision detection for the player avatar.
public class CollisionDetection : MonoBehaviour
{
    private PlayerController playerController;
    //public GameObject mainController;
    public MainController mainController;

    public Enemy enemyScript;
   

    private void Awake()
    {
         playerController = GetComponent<PlayerController>();
    }

    // Colliders are here, see below for triggers

    private void OnCollisionEnter(Collision other)
    {
        // Detects collision with hazards and normal enemies and initial collision with the ground
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
        // If enemy, run Directional collision detection.
        if (other.gameObject.tag == "enemy")
        {
            enemyScript = other.gameObject.GetComponent<Enemy>();
            DirectionalHit(other);   
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

    // Checks for angle of collision from player
    private void DirectionalHit(Collision col)
    {
        for (int k = 0; k < col.contacts.Length; k++)
        {
            // if player hits danger zone
            if (Vector3.Angle(col.contacts[k].normal, enemyScript.validDirection) <= enemyScript.contactThreshold)
            {
                DangerZoneHit(col, enemyScript.directionInverse, enemyScript.canDie);
            }
            // if player hits anywhere else
            else
            {
                BodyHit(col, enemyScript.directionInverse, enemyScript.canDie);
            }
        }
    }

    // Script for hitting danger zone
    private void DangerZoneHit(Collision col, bool dInvert, bool cDie)
    {
        if (dInvert)
        {
            playerController.HazardCollide();
        }
        else
        {
            if (cDie)
            {
                col.gameObject.SetActive(false);
            }
        }
    }
    
    // Script for hitting anywhere other than danger zone
    private void BodyHit(Collision col, bool dInvert, bool cDie)
    {
        if (!dInvert)
        {
            playerController.HazardCollide();
        }
        else
        {
            if (cDie)
            {
                col.gameObject.SetActive(false);
            }
        }
    }

    // Triggers begin here

    private void OnTriggerEnter(Collider other)
    {
        // Detects hitting the goal box and killzone
        if (other.gameObject.name == "Goal")
        {
            mainController.GameWin();
        }
        if (other.gameObject.tag == "killzone")
        {
            mainController.GameOver();
        }
    }
}
