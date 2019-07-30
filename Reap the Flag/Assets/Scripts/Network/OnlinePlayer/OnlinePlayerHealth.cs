using PlayerComponent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlinePlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.7f);     // The colour the damageImage is set to, to flash.
    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    OnlinePlayerController playerMovement;                              // Reference to the player's movement.
    OnlinePlayerFire playerShooting;
    GameStateMachine stateMachine;
    public ParticleSystem particleHits;

    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.
    Color defaultColor = new Color(1f, 0f, 0f, 0f);
    float recoverSpeed = 5.0f;
    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<OnlinePlayerController>();
        playerShooting = GetComponentInChildren<OnlinePlayerFire>();
        // Set the initial health of the player.
        currentHealth = startingHealth;
        stateMachine = GameObject.Find("/NetworkTesting/Communicator").GetComponent<GameStateMachine>();
    }

    void Update()
    {
        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;


        // Play the hurt sound effect.
        playerAudio.Play();
        if (particleHits == null)
            Debug.Log("Player hits!");
        particleHits.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }


    public void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        playerShooting.DisableEffects();

        // Tell the animator that the player is dead.
        anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play();

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        TakeDamage(amount);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
