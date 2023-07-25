using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Scene scene = SceneManager.GetActiveScene();
        string scene1 = "Scene1";
        string scene2 = "Scene2";
        if (scene.name == scene1)
        {
            SceneManager.LoadScene("Scene2");
        }
        if (scene.name == scene2)
        {
            SceneManager.LoadScene("Scene1");
        }
    }
}
