using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows the camera to follow the player
public class CameraController : MonoBehaviour
{
    private GameObject playerChar;

    private Vector3 playerPos;
    private Vector3 cameraPos;

    private void Start()
    {
        TrackingInitialise(); // ABSTRACTION
    }

    private void TrackingInitialise()
    {
        playerChar = GameObject.Find("Player");
        playerPos = playerChar.transform.position;
    }

    private void LateUpdate()
    {
        CameraTracking(); // ABSTRACTION
    }

    private void CameraTracking()
    {
        playerPos = playerChar.transform.position;
        cameraPos = new Vector3(playerPos.x + 22.5f, playerPos.y, playerPos.z);
        transform.position = cameraPos;
    }
}
