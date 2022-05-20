using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for pickup items, handles the animation (applied to all), and declares a powerup method
public abstract class Pickup : MonoBehaviour
{
    protected GameObject player;
    protected PlayerController playerController;
    
    protected int rotationSpeed = 60;
    protected float floatTime = 3f;
    protected float floatPauseTime = 1f;
    protected float floatMax = 0.5f;
    protected Vector3 pickupStartPos;
    protected Vector3 pickupEndPos;

    // Found great way to move object back and forth, thanks MD_Reptile for finding it on the Unity forums
    // Was tempted to install a tweening plugin but I'd prefer to stick to the vanilla Unity tools for this exercise


    protected IEnumerator MoveTimer()
    {
        
        pickupStartPos = transform.position;
        pickupEndPos = pickupStartPos;
        pickupEndPos.y += floatMax;

        while (true)
        {
            yield return StartCoroutine(MovePickup(transform, pickupEndPos, pickupStartPos, floatTime));
            yield return new WaitForSeconds(floatPauseTime);
            yield return StartCoroutine(MovePickup(transform, pickupStartPos, pickupEndPos, floatTime));
            yield return new WaitForSeconds(floatPauseTime);
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

    protected virtual void PickupRotation()
    {
        transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
    }

    // Hopefully this will work for polymorphism

    protected abstract void PowerUp();

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            player = other.gameObject;
            PowerUp();
        }
    }
}
