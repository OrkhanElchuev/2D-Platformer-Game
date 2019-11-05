using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Load first Level
    public void StartFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}
