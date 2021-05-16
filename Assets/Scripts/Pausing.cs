using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pausing : MonoBehaviour
{
    static bool m_isPaused = false;
    // Start is called before the first frame update
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0 && !transform.GetChild(2).gameObject.activeSelf)
        {


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (m_isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        m_isPaused = false;
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        m_isPaused = true;
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public bool GetPausedState()
    {
        return m_isPaused;
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
