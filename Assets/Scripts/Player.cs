using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float runningSpeed = 5f;
    [SerializeField] float jumpingSpeed = 5f;

    // Cached components
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;

    // States
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        InitialAssignments();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRun();
        PlayerJump();
        FlipPlayerSprite();
    }

    // Assigning components
    private void InitialAssignments()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Jump the player
    private void PlayerJump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpingSpeed);
            playerRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    // Run the player
    private void PlayerRun()
    {
        // Value in a range -1 to 1 in Game Configuration file
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        // Define player velocity
        Vector2 playerVelocity = new Vector2(controlThrow * runningSpeed, playerRigidBody.velocity.y);
        // Set the palyer velocity
        playerRigidBody.velocity = playerVelocity;

        bool playerIsMoving = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        // Execute running animation 
        playerAnimator.SetBool("Running", playerIsMoving);
    }

    // Flip sprite according to the direction of player
    private void FlipPlayerSprite()
    {
        // Check if player is moving 
        bool playerIsMoving = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerIsMoving)
        {
            // Mathf.Sign returns 1 if value is positive or 0, and -1 if it is negative
            // Moving to the right would give positive velocity(To the left is negative)
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }
    }
}
