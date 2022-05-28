using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for enemy types
// All enemies: patrol between two points, collides with player for an effect
public abstract class Enemy : MonoBehaviour
{
    // For accessing base coroutine script
    [SerializeField] protected GameObject coControl;
    protected CoroutineController coControlScript;

    // For giving coroutinecontroller the gameobject transform info.
    private GameObject _enemyObj;
    public GameObject enemyObj
    {
        get { return _enemyObj; }
        protected set { _enemyObj = value; }
    }

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

    // For enemies that might pause between movements (similar to pickups)
    private bool pause = false;
    private float pauseTime = 0f;

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

    protected virtual void Start()
    {
        SetPatrolPath();
        StartCoroutine(coControlScript.MoveTimer(enemyObj, hasDelay, delayTime, pause, pauseTime, doesRotate, patrolRotate, patrolA, patrolB, enemyMoveTime));
    }

    // Sets the patrol points then deactivates the empty gameobjects
    protected void SetPatrolPath()
    {
        coControl = GameObject.Find("CoroutineController");
        coControlScript = coControl.gameObject.GetComponent<CoroutineController>();
        patrolA = pointA.transform.position;
        patrolB = pointB.transform.position;
        pointA.SetActive(false);
        pointB.SetActive(false);
    }
}
