using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour
{
    [SerializeField] float runningSpeed = 5f;
    Rigidbody2D playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRun();
        FlipPlayerSprite();
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
    }

    // Flip sprite according to the direction of player
    private void FlipPlayerSprite()
    {
        // Check if player is moving 
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            // Change the sign of player X Scale according to the direction
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }
    }
}
