using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows the camera to follow the player
public class CameraController : MonoBehaviour
{
    public GameObject playerChar;
    public GameObject mainCamera;

    private Vector3 playerPos;
    private Vector3 cameraPos;

    private void Start()
    {
        playerPos = playerChar.transform.position;
    }

    private void LateUpdate()
    {
        playerPos = playerChar.transform.position;
        cameraPos = new Vector3(playerPos.x + 22.5f, playerPos.y, playerPos.z);
        transform.position = cameraPos;
    }
}
