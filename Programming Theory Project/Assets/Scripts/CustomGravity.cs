using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Finding it a bit fiddly to wrangle the gravity effect I want via the Rigidbody, googled a solution
[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    //4x gravity with current jump force gives the nice weighted but bouncy effect I want.
    protected float gravityScale = 4.0f;
    public static float globalGravity = -9.81f;

    Rigidbody m_rb;

    private void OnEnable()
    {
        GravityInitialise(); // ABSTRACTION
    }

    private void GravityInitialise()
    {
        // Gets RB component upon loading object and turns off 
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }

    void FixedUpdate()
    {
        GravityForce(); // ABSTRACTION
    }

    private void GravityForce()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
