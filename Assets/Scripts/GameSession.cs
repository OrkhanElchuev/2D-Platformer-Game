using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    private void Awake()
    {
        // Get the number of GameSession objects
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        // If there are more than one destroy itself
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // Keep the gameSession object otherwise
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Load start menu and destroy gameSession object
    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void DecrementLife()
    {
        playerLives--;
        // Assign current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ExecutePlayerDeath()
    {
        if (playerLives > 1)
        {
            // Take 1 life from player 
            DecrementLife();
        }
        else
        {
            ResetGameSession();
        }
    }
}
