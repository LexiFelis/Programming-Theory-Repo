using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For controlling the player character and tracking lives.
public class PlayerController : MonoBehaviour
{
    public int playerLifeMax { get; private set; }

    public int playerLivesRemain;

    private float runSpeed = 20;
    private float jumpForce = 100;
    private int goForward = 1;
    private int cooldownTime = 3;

    private Rigidbody playerRb;

    private GameObject mainControl;
    private MainController mainController;

    // Damage cooldown and animation
    private Renderer rend;
    public bool grounded;
    public bool hitCooldown;
    public static bool isPlaying;

    [SerializeField] private ParticleSystem invPart;
    [SerializeField] private GameObject invAura;

    void Start()
    {
        PlayerInitialize();
    }

    void PlayerInitialize()
    {
        mainControl = GameObject.Find("MainController");
        mainController = mainControl.GetComponent<MainController>();
        playerLifeMax = 3;
        playerLivesRemain = playerLifeMax;
        playerRb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        grounded = false;
        isPlaying = true;
        hitCooldown = false;
        invAura.SetActive(false);
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

    // Blinking animation for damage frames
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

    // Method to start coroutine is needed in here as the pickup disables itself, stopping the coroutine.
    public void InvincibilityTrigger(float invTime)
    {
        StartCoroutine(Invincibility(invTime));
    }

    // Effectively a fancy hitCooldown with particle effect and aura.
    private IEnumerator Invincibility(float invTime)
    {
        hitCooldown = true;
        invAura.SetActive(true);
        invPart.Play();

        yield return new WaitForSeconds(invTime);

        invPart.Stop();
        invAura.SetActive(false);
        hitCooldown = false;

        yield return null;
    }
}
