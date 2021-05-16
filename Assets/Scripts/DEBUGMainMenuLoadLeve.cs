using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUGMainMenuLoadLeve : MonoBehaviour
{
    private void Update()
    {
        //kamil Scene
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SceneManager.LoadScene(3);
        }
        //Alex Scene
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(4);
        }
    }
}
