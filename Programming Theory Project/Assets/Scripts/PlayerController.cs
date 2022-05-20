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

    // Damage cooldown and animation
    public Renderer rend;
    public bool grounded;
    public bool hitCooldown;
    public static bool isPlaying;


    void Start()
    {
        playerLivesRemain = playerLifeMax;
        playerRb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
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
        if (!hitCooldown)
        {
            PlayerDamage();
            playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            StartCoroutine(HitCooldown(cooldownTime));
        }

    }

    // takes one life from player, runs GameOver if lives drop to zero.
    void PlayerDamage()
    {
        if (isPlaying)
        {
            hitCooldown = true;
            grounded = true;
            playerLivesRemain--;
            if (playerLivesRemain <= 0)
            {
                mainController.GameOver();
            }
        }
    }

    // Starts blinking animation for cooldown duration, stops blinking, ensures player is visible, then turns itself off.
    IEnumerator HitCooldown(int w)
    {
        while (hitCooldown == true)
        {
            InvokeRepeating("Blink", 0, 0.10f);            
            yield return new WaitForSeconds(w);
            CancelInvoke("Blink");
            rend.enabled = true;
            hitCooldown = false;
        }
    }

    // Blinking animation for invincibility frames
    void Blink()
    {
        if (rend.enabled == true)
        {
            rend.enabled = false;
        }
        else
        {
            rend.enabled = true;
        }
    }

}
