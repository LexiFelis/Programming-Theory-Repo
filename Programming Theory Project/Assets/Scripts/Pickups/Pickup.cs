using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for pickup items, handles the animation (applied to all), and declares a powerup method
public abstract class Pickup : MonoBehaviour
{
    protected GameObject player;
    protected PlayerController playerController;

    // For giving CoroutineController the gameobject transform info.
    private GameObject _pickUp;
    public GameObject pickUp
    {
        get { return _pickUp; }
        protected set { _pickUp = value; }
    } // ENCAPSULATION

    // For accessing base coroutine script
    [SerializeField] protected GameObject coControl;
    protected CoroutineController coControlScript;

    // variables for coroutine controller
    protected Vector3 pickupStartPos;
    protected Vector3 pickupEndPos;

    // baked in variables for coroutine controller
    protected int rotationSpeed = 60;
    protected float floatTime = 3f;
    protected bool doesFloat = true;
    protected float floatPauseTime = 1f;
    protected float floatMax = 0.5f;

    // baked in variables for coroutine controller that don't apply to pickups.
    private bool delay = false;
    private float delayTime = 0f;
    private bool doesRotate = false;
    protected Vector3 rotateBlank;


    protected virtual void Start()
    {
        InitialiseRoute(); // ABSTRACTION
        StartCoroutine(coControlScript.MoveTimer(pickUp, delay, delayTime, doesFloat, floatPauseTime, doesRotate, rotateBlank, pickupStartPos, pickupEndPos, floatTime));
    }

    protected void InitialiseRoute()
    {
        coControl = GameObject.Find("CoroutineController");
        coControlScript = coControl.gameObject.GetComponent<CoroutineController>();
        pickupStartPos = transform.position;
        pickupEndPos = pickupStartPos;
        pickupEndPos.y += floatMax;
    }

    // Slowly rotates the pickup
    protected virtual void Update()
    {
        PickupRotation(); // ABSTRACTION
    }

    protected virtual void PickupRotation()
    {
        transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
    }

    // abstract method for handling effects on pickup
    protected abstract void PowerUp();

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            player = other.gameObject;
            PowerUp(); // INHERITENCE
        }
    }
}
