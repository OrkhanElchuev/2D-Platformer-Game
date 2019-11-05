using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;

    // When player collides with Level Exit sign execute LoadNextLevel
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }

    // Load Next Level after defined delay (2f)
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        // Get the current scene index and assign it
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Load next scene
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
