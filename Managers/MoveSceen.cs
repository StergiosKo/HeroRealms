using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceen : MonoBehaviour
{
    public void Pressed(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
