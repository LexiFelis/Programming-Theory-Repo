using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For controlling the player character and tracking lives.
public class PlayerController : MonoBehaviour
{
    // These two will be privated eventually with get/set
    public int playerLifeMax = 1;
    public int playerLivesRemain;

    private float runSpeed = 20;
    private float jumpForce = 100;
    private int goForward = 1;
    private int cooldownTime = 3;

    private Rigidbody playerRb;

    public MainController mainController;

    public bool grounded;
    public bool hitCooldown;
    public static bool isPlaying;

    private IEnumerator cooldownCoroutine;

    void Start()
    {
        playerLivesRemain = playerLifeMax;
        playerRb = GetComponent<Rigidbody>();
        grounded = false;
        isPlaying = true;
        hitCooldown = false;
    }


    void Update()
    {
        if (isPlaying)
        {
            PlayerControls();
        }
    }

    void PlayerControls()
    {
        if (Input.GetKey(KeyCode.D))
        {
            PlayerRun(goForward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerRun(-goForward);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }
    }

    void PlayerRun(int i)
    {
        playerRb.AddForce((Vector3.forward * runSpeed * i), ForceMode.Force);
    }

    void PlayerJump()
    {
        if (grounded)
        {
            playerRb.AddForce((Vector3.up * jumpForce), ForceMode.Impulse);
        }
        grounded = false;
    }

    public void HazardCollide()
    {
        //Damages player, allows a jump to escape, sets cooldown timer before player can be hit again
        PlayerDamage();
        hitCooldown = true;
        grounded = true;
        cooldownCoroutine = HitCooldown(cooldownTime);
        StartCoroutine(cooldownCoroutine);
    }

    void PlayerDamage()
    {
        if (isPlaying)
        {
            playerLivesRemain--;
            if (playerLivesRemain <= 0)
            {
                mainController.GameOver();
            }
        }
    }

    IEnumerator HitCooldown(int w)
    {
        while (true)
        {
            yield return new WaitForSeconds(w);
            hitCooldown = false;
        }
    }
}
