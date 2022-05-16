using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for enemy types
// All enemies: patrol between two points, collides with player for an effect
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected bool canDie;
    [SerializeField] protected float enemyMoveTime = 4f;

    // Vectors for enemies to move between, can be set in 3D space via editor.
    [SerializeField] protected GameObject pointA;
    protected Vector3 patrolA;
    [SerializeField] protected GameObject pointB;
    protected Vector3 patrolB;

    // For enemies that rotate when patrolling
    protected bool doesRotate;
    protected Vector3 patrolRotate;

    // for directional collision detection
    protected float contactThreshold;
    protected Vector3 validDirection;

    // for accessing player script
    protected GameObject playerObject;
    protected PlayerController playerController;

    protected virtual void Start()
    {
        PlayerSet();
        StartCoroutine(MoveTimer());
    }

    protected void SetPatrolPath()
    {
        patrolA = pointA.transform.position;
        patrolB = pointB.transform.position;
        pointA.SetActive(false);
        pointB.SetActive(false);
    }

    protected void PlayerSet()
    {
        playerObject = GameObject.Find("Player");
        playerController = playerObject.GetComponent<PlayerController>();
    }

    // Similar coroutine as the pickup animation, could possibly be merged into a single class that handles moving obstacles/enemies?
    // if(doesRotate) exists for patrolling enemies that turn around, like the knife.
    protected IEnumerator MoveTimer()
    {
        SetPatrolPath();

        while (true)
        {
            if (doesRotate)
            {
                gameObject.transform.Rotate(patrolRotate);
            }
            yield return StartCoroutine(MovePickup(transform, patrolA, patrolB, enemyMoveTime));
            if (doesRotate)
            {
                gameObject.transform.Rotate(patrolRotate);
            }
            yield return StartCoroutine(MovePickup(transform, patrolB, patrolA, enemyMoveTime));
        }
    }

    protected IEnumerator MovePickup(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    // If enemy can die, run Directional collision detection. If not, hurt player.
    protected virtual void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            if (canDie)
            {
                DirectionalHit(col);
            }
            else
            {
                playerController.HazardCollide();
            }
        }
    }

    // Checks for angle of collision from player, then either damages or gets deactivated
    protected void DirectionalHit(Collision col)
    {
        for (int k = 0; k < col.contacts.Length; k++)
        {
            // checks if colliding with bottom of player, then deactivates itself
            if (Vector3.Angle(col.contacts[k].normal, validDirection) <= contactThreshold)
            {
                gameObject.SetActive(false);
                Debug.Log("Boop!");
            }
            else
            {
                playerController.HazardCollide();
            }
        }
    }
}
