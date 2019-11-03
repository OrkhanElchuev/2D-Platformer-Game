using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float runningSpeed = 5f;
    [SerializeField] float jumpingSpeed = 5f;
    [SerializeField] float climbingSpeed = 5f;

    // Cached components
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    Collider2D playerCollider2D;

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
        ClimbLadder();
        FlipPlayerSprite();
    }

    // Assigning components
    private void InitialAssignments()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider2D = GetComponent<Collider2D>();
    }

    // Jump the player
    private void PlayerJump()
    {
        // If player is not colliding with ground then return
        // Created to avoid multiple jumps at once
        if (!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        // If SPACE key is pressed 
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            // Change Y coordinate of player
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpingSpeed);
            playerRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder()
    {
        // If player is not colliding with ladder then return 
        if (!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            return;
        }
        // Value in a range -1 to 1 in Game Configuration file
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        // Define player climbing velocity
        Vector2 climbingVelocity = new Vector2(playerRigidBody.velocity.x, controlThrow * climbingSpeed);
        playerRigidBody.velocity = climbingVelocity;
        // Check if player is moving vertically
        bool playerIsMovingVer = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        // Execute climbing animation 
        playerAnimator.SetBool("Climbing", playerIsMovingVer);
    }

    // Run the player
    private void PlayerRun()
    {
        // Value in a range -1 to 1 in Game Configuration file
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        // Define player movement velocity
        Vector2 playerVelocity = new Vector2(controlThrow * runningSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;
        // Check if player is moving horizontally
        bool playerIsMovingHor = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        // Execute running animation 
        playerAnimator.SetBool("Running", playerIsMovingHor);
    }

    // Flip sprite according to the direction of player
    private void FlipPlayerSprite()
    {
        // Check if player is moving horizontally
        bool playerIsMovingHor = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerIsMovingHor)
        {
            // Mathf.Sign returns 1 if value is positive or 0, and -1 if it is negative
            // Moving to the right would give positive velocity(To the left is negative)
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }
    }
}
