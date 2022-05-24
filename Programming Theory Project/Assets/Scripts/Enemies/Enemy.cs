using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for enemy types
// All enemies: patrol between two points, collides with player for an effect
public abstract class Enemy : MonoBehaviour
{
    // Start of enemy movement variables.
    [SerializeField] protected float enemyMoveTime = 4f;

    // For enemies that have initial delay
    protected bool hasDelay = false;
    private float _delayTime = 0;
    protected float delayTime
    {
        get => _delayTime;
        set
        {
            if ((value >= 0) && (value < enemyMoveTime * 2))
            {
                _delayTime = value;
            }
            else { Debug.LogError(gameObject.name + ": invalid delay time"); }
        }
    }

    // Vectors for enemies to move between, can be set in 3D space via editor.
    [SerializeField] protected GameObject pointA;
    protected Vector3 patrolA;
    [SerializeField] protected GameObject pointB;
    protected Vector3 patrolB;

    // For enemies that rotate when patrolling
    protected bool doesRotate;
    protected Vector3 patrolRotate;

    // for directional collision detection, mandatory for all children
    protected bool m_canDie;
    public abstract bool canDie { get; protected set; }
    protected bool m_directionInverse;
    public abstract bool directionInverse { get; protected set; }
    protected float m_contactThreshold;
    public abstract float contactThreshold { get; protected set; }
    protected Vector3 m_validDirection;
    public abstract Vector3 validDirection { get; protected set; }

    // for accessing player script (may be used for enemies that chase)
    //protected GameObject playerObject;
    //protected PlayerController playerController;

    protected virtual void Start()
    {
        //PlayerSet();
        StartCoroutine(MoveTimer());
    }

    // Sets the patrol points then deactivates the empty gameobjects
    protected void SetPatrolPath()
    {
        patrolA = pointA.transform.position;
        patrolB = pointB.transform.position;
        pointA.SetActive(false);
        pointB.SetActive(false);
    }

    //protected void PlayerSet()
    //{
    //    playerObject = GameObject.Find("Player");
    //    playerController = playerObject.GetComponent<PlayerController>();
    //}

    // Similar coroutine as the pickup animation, could possibly be merged into a single class that handles moving obstacles/enemies?
    // if(doesRotate) exists for patrolling enemies that turn around, like the knife.
    protected IEnumerator MoveTimer()
    {
        SetPatrolPath();
        if (hasDelay == true)
        {
            yield return new WaitForSeconds(_delayTime);
        }

        while (true)
        {
            if (doesRotate)
            {
                gameObject.transform.Rotate(patrolRotate);
            }
            yield return StartCoroutine(MoveEnemy(transform, patrolA, patrolB, enemyMoveTime));
            if (doesRotate)
            {
                gameObject.transform.Rotate(patrolRotate);
            }
            yield return StartCoroutine(MoveEnemy(transform, patrolB, patrolA, enemyMoveTime));
        }
    }

    protected IEnumerator MoveEnemy(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
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
}
